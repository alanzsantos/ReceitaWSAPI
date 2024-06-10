using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReceitaWSAPI.Data;
using ReceitaWSAPI.Middleware;
using ReceitaWSAPI.Models;
using ReceitaWSAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner.
builder.Services.AddControllers();
builder.Services.AddDbContext<ReceitaWSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar ReceitaWsService com HttpClient
builder.Services.AddHttpClient<ReceitaWsService>();

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adicionar configura��o para autentica��o b�sica
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configurar o pipeline de requisi��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adicionar o middleware de autentica��o b�sica antes de usar a autoriza��o
app.UseMiddleware<BasicAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
