// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Discount.Api.Protos;
using EventBus.Messages.Events;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Order.Api.EventBusConsummers;
using Order.Api.Middlewares;
using Order.Application;
using Order.Application.Extensions;
using Order.Application.Services;
using Order.Core.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.ExternalServices;
using Order.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqloptions =>
    {
        sqloptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

builder.Services.AddGrpcClient<DiscountServicegRPC.DiscountServicegRPCClient>(options =>
{
    options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl"));
});

builder.Services.AddMassTransit(config =>
{
    
    config.AddConsumer<CardAddOrderConsumer>();
    config.AddConsumer<UpdateOrderStatusConsumer>();
    config.UsingRabbitMq((context, configurator) =>
    {   
        configurator.Host(builder.Configuration["EventBusSettings:HostAddress"], c =>
        {
            c.Username(builder.Configuration["EventBusSettings:UserName"]!);
            c.Password(builder.Configuration["EventBusSettings:Password"]!);
        });
       // configurator.ConfigureEndpoints(context);
        configurator.ReceiveEndpoint(EventBus.Messages.Commons.EventBusConstant.CartCheckoutQueue , c =>
        {
            c.ConfigureConsumer<CardAddOrderConsumer>(context);
        });
        configurator.ReceiveEndpoint(EventBus.Messages.Commons.EventBusConstant.UpdateOrderStatusQueue, c =>
        {
            c.ConfigureConsumer<UpdateOrderStatusConsumer>(context);
        });
    });
});

builder.Services.AddApplication();
builder.Services.AddTransient<CardAddOrderConsumer>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IDiscountClientService, DiscountClientService>();
//builder.Services.AddScoped<CleanupExpiredOrdersUseCase>();
//builder.Services.AddHostedService<OrderCleanupBackgroudService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddHealthChecks()
 .AddSqlServer(
     connectionString: builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new Exception("SQL Server connection string is missing"),
     name: "sqlserver",
     failureStatus: HealthStatus.Degraded,
     timeout: TimeSpan.FromSeconds(5));

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<OrderDbContext>();

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

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
