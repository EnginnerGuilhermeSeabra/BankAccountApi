using Microsoft.EntityFrameworkCore;
using TES.Domain.Accounts.Entities;
using TES.Infrastructure.Persistence.Context.Configurations;

namespace TES.Infrastructure.Persistence.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
    }
}
