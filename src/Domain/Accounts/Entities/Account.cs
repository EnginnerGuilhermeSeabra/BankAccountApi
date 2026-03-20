using TES.Domain.Accounts.Enums;
using TES.Domain.Accounts.Events;
using TES.Domain.Accounts.ValueObjects;
using TES.Domain.Common;

namespace TES.Domain.Accounts.Entities;

/// <summary>
/// Aggregate Root do contexto de Contas Bancárias.
/// Toda mutação de estado passa obrigatoriamente pelos métodos desta classe,
/// garantindo invariantes do domínio e disparando Domain Events.
/// </summary>
public sealed class Account : BaseEntity
{
    public Guid Id { get; private set; }
    public string NomeTitular { get; private set; }
    public Cpf Cpf { get; private set; }
    public AccountStatus Status { get; private set; }
    public AccountEvent LastEvent { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime? AtualizadoEm { get; private set; }

    // EF Core requires parameterless constructor
    private Account() { NomeTitular = string.Empty; Cpf = null!; }

    private Account(Guid id, string nomeTitular, Cpf cpf)
    {
        Id = id;
        NomeTitular = nomeTitular;
        Cpf = cpf;
        Status = AccountStatus.Ativa;
        LastEvent = AccountEvent.Created;
        CriadoEm = DateTime.UtcNow;

        AddDomainEvent(new AccountCreatedEvent(Id, NomeTitular, Cpf.Value));
    }

    public static Account Create(string nomeTitular, string cpf)
    {
        if (string.IsNullOrWhiteSpace(nomeTitular))
            throw new DomainException("Nome do titular não pode ser vazio.");

        var cpfVo = Cpf.Create(cpf);
        return new Account(Guid.NewGuid(), nomeTitular.Trim(), cpfVo);
    }

    public void Atualizar(string nomeTitular, AccountStatus status)
    {
        if (string.IsNullOrWhiteSpace(nomeTitular))
            throw new DomainException("Nome do titular não pode ser vazio.");

        NomeTitular = nomeTitular.Trim();
        Status = status;
        LastEvent = AccountEvent.Updated;
        AtualizadoEm = DateTime.UtcNow;

        AddDomainEvent(new AccountUpdatedEvent(Id, NomeTitular, Status));
    }

    public void Inativar()
    {
        if (Status == AccountStatus.Inativa)
            throw new DomainException("Conta já está inativa.");

        Status = AccountStatus.Inativa;
        LastEvent = AccountEvent.Deleted;
        AtualizadoEm = DateTime.UtcNow;

        AddDomainEvent(new AccountDeletedEvent(Id));
    }
}
