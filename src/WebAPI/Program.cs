using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TES.Application.Accounts.Commands.CreateAccount;
using TES.Application.Common.Interfaces;
using TES.Domain.Accounts.Repositories;
using TES.Infrastructure.Cache;
using TES.Infrastructure.Persistence.Context;
using TES.Infrastructure.Persistence.Repositories;
using TES.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ──────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TES - Fintech API", Version = "v1" });
    c.EnableAnnotations();
});

// ── MediatR (CQRS + Domain Events) ───────────────────────────────────
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).Assembly));

// ── FluentValidation ─────────────────────────────────────────────────
builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();

// ── EF Core In-Memory ────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseInMemoryDatabase("TesDb"));

// ── Repositórios ─────────────────────────────────────────────────────
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// ── Cache (IMemoryCache + Serviço de Cache) ───────────────────────────
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IAccountCacheService, AccountCacheService>();

var app = builder.Build();

// ── Middleware Pipeline ───────────────────────────────────────────────
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

// Necessário para testes de integração
public partial class Program { }
