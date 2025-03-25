// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Gateway.ModelCustom;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// cấu hình cho phép gateway nhận mọi request 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis-container:6379"; 
});

builder.Services.AddHttpClient();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();

            var isRevoked = await cache.GetStringAsync($"blacklist:{token}");
            if (isRevoked == "true")
            {
                context.Fail("Token has been revoked.");
                return;
            }

            if(isRevoked == null)
            {
                var httpClient = context.HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient();
                var response = await httpClient.GetAsync($"{builder.Configuration["IdentityProvider:Url"]}/check-token?token={token}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CheckTokenResponse>();
                    if (result?.IsRevoke == true)
                    {
                        // Lưu vào cache với TTL 15 phút (khớp AccessTokenLifetime)
                        await cache.SetStringAsync($"blacklist:{token}", "true", new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                        });
                        context.Fail("Token has been revoked.");
                    }
                }
            }
        }
    };
});


// Add Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
