using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReceitaWSAPI.Data;
using ReceitaWSAPI.Middleware;
using ReceitaWSAPI.Models;
using ReceitaWSAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddDbContext<ReceitaWSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar ReceitaWsService com HttpClient
builder.Services.AddHttpClient<ReceitaWsService>();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adicionar configuração para autenticação básica
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configurar o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adicionar o middleware de autenticação básica antes de usar a autorização
app.UseMiddleware<BasicAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
