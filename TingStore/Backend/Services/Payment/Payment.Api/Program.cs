// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Payment.Application;

using Payment.Core.Repositories;
using Payment.Infrastructure.Data;

using Payment.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(80);
//});

builder.Services.AddControllers();


builder.Services.AddDbContext<PaymentDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration["EventBusSettings:HostAddress"], c =>
        {
            c.Username(builder.Configuration["EventBusSettings:UserName"]!);
            c.Password(builder.Configuration["EventBusSettings:Password"]!);
        });


        configurator.ConfigureJsonSerializerOptions(options =>
        {
            options.PropertyNameCaseInsensitive = true;
            options.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
            options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            return options;
        });
        configurator.ConfigureEndpoints(context);
    });
});


builder.Services.AddApplication();
builder.Services.AddScoped<VNPAY.NET.IVnpay, VNPAY.NET.Vnpay>();
builder.Services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

builder.Services.AddHealthChecks()
 .AddSqlServer(
     connectionString: builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new Exception("SQL Server connection string is missing"),
     name: "sqlserver",
     failureStatus: HealthStatus.Degraded,
     timeout: TimeSpan.FromSeconds(5));

//builder.Services.AddApp
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<PaymentDbContext>();

    try
    {
        dbContext.Database.Migrate();

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error create database");
    }
}


app.UseSwagger();
app.UseSwaggerUI();


app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
