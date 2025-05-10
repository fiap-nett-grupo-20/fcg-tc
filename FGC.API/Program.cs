using Microsoft.EntityFrameworkCore;
using FGC.API.Data;
using Microsoft.AspNetCore.Identity;
using FGC.API.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DbIdentityLoginContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbIdentityLoginContext")
    ?? throw new InvalidOperationException("Connection string 'DbIdentityLoginContext' não encontrada.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;          
    options.Password.RequireLowercase = true;      
    options.Password.RequireUppercase = true;       
    options.Password.RequireNonAlphanumeric = true; 
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;

})
.AddEntityFrameworkStores<DbIdentityLoginContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); 
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers(); 

app.Run();