using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Thi.ProjetoLivraria.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuracao dos Controllers e Views
builder.Services.AddControllersWithViews();

// Configuracao da conexao com MySQL
var connectionString = builder.Configuration
    .GetConnectionString("ConexaoMySQL");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "A conexao com o MySQL nao foi configurada."
    );
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// Servico para protecao e verificacao de senhas
builder.Services.AddScoped<
    IPasswordHasher<Login>,
    PasswordHasher<Login>
>();

// Configuracao do armazenamento de sessao
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;
});

// Construindo a aplicacao
var app = builder.Build();

// Configuracoes de ambiente
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Habilitando sessoes
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conta}/{action=Login}/{id?}"
);

app.Run();