using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TES.Application.Accounts.Commands.CreateAccount;
using TES.Application.Common.Interfaces;
using TES.Domain.Accounts.Repositories;
using TES.Infrastructure.Cache;
using TES.Infrastructure.Persistence.Context;
using TES.Infrastructure.Persistence.Repositories;
using TES.WebMVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ── MVC - Models, Views, Controllers ─────────────────────────────────
builder.Services.AddControllersWithViews();

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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
