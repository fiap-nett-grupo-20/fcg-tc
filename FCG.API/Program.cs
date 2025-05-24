using FCG.Application.Middleware;
using FCG.Application.Services;
using FCG.Application.Services.Interfaces;
using FCG.Domain.Interfaces;
using FCG.Infra.Data.Context;
using FCG.Infra.Data.Repository;
using FCG.Infra.Data.Seedings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<FCGDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FCG"),
    b => b.MigrationsAssembly(typeof(FCGDbContext).Assembly));
}, ServiceLifetime.Scoped);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<FCGDbContext>();
    bool databaseWasCreated = await dbContext.Database.EnsureCreatedAsync();
    Console.WriteLine(databaseWasCreated ? "Database created." : "Database already exists.");


    await GameSeeding.SeedAsync(dbContext);
}

app.UseDeveloperExceptionPage();
app.UseExceptionHandler("/Error");
app.UseHsts();


app.UseHttpsRedirection();


//app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();