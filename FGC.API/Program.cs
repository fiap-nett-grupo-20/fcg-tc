using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FGC.API.Models;
using FGC.API.Controllers;
using FGC.API.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbIdentityLoginContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbFGCAPIContext")
    ?? throw new InvalidOperationException("Connection string 'DbFGCAPIContext' not found.")));


builder.Services.AddDbContext<DbIdentityLoginContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbIdentityLoginContext")
    ?? throw new InvalidOperationException("Connection string 'DbIdentityLoginContext' not found.")));



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*Configurar o identity*/
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<DbIdentityLoginContext>()
.AddDefaultTokenProviders();

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

//app.MapUsersEndpoints();

app.Run();
