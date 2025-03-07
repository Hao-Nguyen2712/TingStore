using System.Reflection;
using MediatR;
using Microsoft.OpenApi.Models;
using Product.Application.Data;
using Product.Application.Handlers;
using Product.Application.Interface;
using Product.Application.Services.ImageCloud;
using Product.Core.Repositories;
using Product.Infrastructure.DbContext;
using Product.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dj
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);
builder.Services.AddScoped<IProductContext, ProductContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IImageManagementService, ImageManagementServices>();
builder.Services.AddScoped<ImageManagementProductServices>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); //add
app.UseAuthentication(); //add
app.UseAuthorization();

app.MapControllers();

app.Run();
