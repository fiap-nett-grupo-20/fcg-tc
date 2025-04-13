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

/*Configurei o identity e depois criei a migration 
add-migration IdentityUsersCreate
Update-Database -Context DbIdentityLoginContext
*/
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    // Exige que a senha contenha pelo menos um dígito numérico (0-9).

    options.Password.RequireLowercase = true;
    // Exige que a senha contenha pelo menos uma letra minúscula (a-z).

    options.Password.RequireUppercase = true;
    // Exige que a senha contenha pelo menos uma letra maiúscula (A-Z).

    options.Password.RequireNonAlphanumeric = true;
    // Exige que a senha contenha pelo menos um caractere não alfanumérico (ex.: !, @, #, $, etc.).

    options.Password.RequiredLength = 8;
    // Define o comprimento mínimo da senha como 8 caracteres.

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
