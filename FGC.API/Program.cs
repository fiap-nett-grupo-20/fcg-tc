using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FGC.API.Models;
using FGC.API.Controllers;
using FGC.API.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FGCAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FGCAPIContext") ?? throw new InvalidOperationException("Connection string 'FGCAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapUsersEndpoints();

//app.MapUsersEndpoints();

app.Run();
