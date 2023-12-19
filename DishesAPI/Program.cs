using AutoMapper;
using DishesAPI.DBContexts;
using DishesAPI.Entities;
using DishesAPI.Extensions;
using DishesAPI.Models;
using DishesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExtensionsMethod(builder.Configuration);
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//Using Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(configureApplictaionBuilder =>
    {
        configureApplictaionBuilder.Run(
            async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("An Exception problem happend");
            });
    });
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterDishesEndpoints();
app.RegisterIngredientsEndpoints();

app.Run();