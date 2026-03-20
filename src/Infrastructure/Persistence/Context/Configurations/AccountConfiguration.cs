using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TES.Domain.Accounts.Entities;

namespace TES.Infrastructure.Persistence.Context.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.NomeTitular)
            .IsRequired()
            .HasMaxLength(150);

        // CPF é um Value Object — mapeia apenas o .Value (string)
        var cpfBuilder = builder.OwnsOne(a => a.Cpf);
        cpfBuilder.Property(c => c.Value)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(a => a.Status).IsRequired();
        builder.Property(a => a.LastEvent).IsRequired();
        builder.Property(a => a.CriadoEm).IsRequired();

        builder.HasIndex(a => a.Id).IsUnique();
    }
}
