
using Discount.Api.Services;
using Discount.Application;
using Discount.Core.Repositories;
using Discount.Infrastructure.Db;
using Discount.Infrastructure.Db.DataSeeding;
using Discount.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(80);
//});

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Discount.API", Version = "v1" });
});

builder.Services.AddGrpcReflection();
builder.Services.AddApplication();
builder.Services.AddDbContext<DiscountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

builder.Services.AddHealthChecks()
 .AddSqlServer(
     connectionString: builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new Exception("SQL Server connection string is missing"),
     name: "sqlserver",
     failureStatus: HealthStatus.Degraded,
     timeout: TimeSpan.FromSeconds(5));

var app = builder.Build();

app.MapSwagger();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DiscountDbContext>();

    try
    {
        dbContext.Database.Migrate();
        CouponSeeding.Seed(dbContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error create database");
    }
}

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGrpcReflectionService();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.API v1");
        c.RoutePrefix = "swagger";
    });
}

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
