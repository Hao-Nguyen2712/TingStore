
using Discount.Api.Services;
using Discount.Application;
using Discount.Core.Repositories;
using Discount.Infrastructure.Db;
using Discount.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddApplication();
builder.Services.AddDbContext<DiscountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqloptions =>
    {
        sqloptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });

});

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGrpcReflectionService();


app.Run();
