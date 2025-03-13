// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;
using Category.Application.Handlers;
using Category.Core.Repositories;
using Category.Infrastructure.Data;
using Category.Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(CreateCategoryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(DeleteCategoryByIdHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(UpdateCategoryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetCategoryByIdHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetCategoryByNameHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetAllCategoriesHandler).GetTypeInfo().Assembly);
builder.Services.AddScoped<ICategoryContext, CategoryContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
