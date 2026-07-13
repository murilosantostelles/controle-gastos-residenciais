using ControleGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ControleGastos.Application.Interfaces;
using ControleGastos.Application.Services;
using ControleGastos.Infrastructure.Repositories;
using ControleGastos.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// repositories
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// services
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<TransacaoService>();
builder.Services.AddScoped<TotaisService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
