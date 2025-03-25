using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using User.Api.Extensions;
using User.Infrastructure.Data;
using User.Infrastructure.Extentions;
using User.Application;
using User.Application.Queries;
using User.Api.Middlewares;
using MediatR;
using User.Application.Behaviors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning();

// Add DbContext and IUserRepository via InfraServices extension
builder.Services.AddInfraServices(builder.Configuration);

// Add MediatR with FluentValidation pipeline behavior
builder.Services.AddValidatorsFromAssemblyContaining<UserApplicationEntryPoint>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(UserApplicationEntryPoint).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(UserApplicationEntryPoint).Assembly);

// Add Validators
builder.Services.AddValidatorsFromAssemblyContaining<UserApplicationEntryPoint>();


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

// Đăng ký DbContext đúng cách
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHealthChecks().Services.AddDbContext<UserContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }


// Migrate database with retry policy and seed data using UserContextSeed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<UserContextSeed>>();
    var context = services.GetRequiredService<UserContext>();

    try
    {
        // Migrate database trước khi seed dữ liệu
        context.Database.Migrate();

        await UserContextSeed.SeedAsync(context, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Add Global Exception Handler
app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.MapControllers();

app.Run();
