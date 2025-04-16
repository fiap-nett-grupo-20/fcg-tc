using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
// using FGC.API.Models; // Removido se não for usado diretamente aqui, mas necessário para ApplicationUser
using FGC.API.Data; // Namespace do seu DbContext e ApplicationUser
using Microsoft.AspNetCore.Identity;
using FGC.API.Models;
using FGC.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// --- Configuração do DbContext ---
// Registra o DbContext para o Identity.
// É crucial que este DbContext herde de IdentityDbContext<ApplicationUser>
builder.Services.AddDbContext<DbIdentityLoginContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbIdentityLoginContext") // Garanta que esta connection string está correta no appsettings.json
    ?? throw new InvalidOperationException("Connection string 'DbIdentityLoginContext' não encontrada.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do ASP.NET Core Identity 
// IMPORTANTE: Alterado de IdentityUser para ApplicationUser para usar a classe personalizada.
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;           // Exige pelo menos um dígito (0-9).
    options.Password.RequireLowercase = true;       // Exige pelo menos uma letra minúscula (a-z).
    options.Password.RequireUppercase = true;       // Exige pelo menos uma letra maiúscula (A-Z).
    options.Password.RequireNonAlphanumeric = true; // Exige pelo menos um caractere não alfanumérico (!, @, #, etc.).
    options.Password.RequiredLength = 8;            // Comprimento mínimo de 8 caracteres.
    options.User.RequireUniqueEmail = true; // Exige que emails sejam únicos (recomendado)

    // Outras opções do Identity
    // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // Tempo de bloqueio após tentativas falhas
    // options.Lockout.MaxFailedAccessAttempts = 5;                      // Nº máximo de tentativas antes de bloquear
    // options.SignIn.RequireConfirmedAccount = false;                  // Define se a conta precisa ser confirmada (email/telefone) para login
    // options.SignIn.RequireConfirmedEmail = false;
    // options.SignIn.RequireConfirmedPhoneNumber = false;
})
// Configura o Entity Framework Core como o armazenamento para o Identity, usando o DbContext especificado.
.AddEntityFrameworkStores<DbIdentityLoginContext>()
// Adiciona os provedores de token padrão (para reset de senha, confirmação de email, etc.).
.AddDefaultTokenProviders();


// --- Autenticação e Autorização ---
// configuração de autenticação.
/*

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true, // Validar quem emitiu o token
        ValidateAudience = true, // Validar para quem o token foi emitido
        ValidateLifetime = true, // Validar se o token não expirou
        ValidateIssuerSigningKey = true, // Validar a assinatura do token
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Emissor configurado no appsettings.json
        ValidAudience = builder.Configuration["Jwt:Audience"], // Audiência configurada no appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Chave secreta
    };
});

builder.Services.AddAuthorization(); // Adiciona o serviço de autorização
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Em desenvolvimento, é útil ter páginas de erro detalhadas.
    app.UseDeveloperExceptionPage();
}
else
{
    // Em produção, use um handler de exceção mais robusto e HSTS.
    app.UseExceptionHandler("/Error");
    app.UseHsts(); 
}

app.UseHttpsRedirection();

// IMPORTANTE: Adicionar UseAuthentication() ANTES de UseAuthorization() quando implementar o JWT.
// app.UseAuthentication(); // Habilita a autenticação. //descomentado para não gerar erro de autenticação no swagger

app.UseAuthorization(); // Habilita a autorização.

app.UseMiddleware<ExceptionMiddleware>(); // Adiciona o middleware de tratamento de exceções.

app.MapControllers(); 

app.Run();