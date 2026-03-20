using MediatR;
using TES.Application.Accounts.DTOs;
using TES.Application.Common.Interfaces;
using TES.Domain.Accounts.Entities;
using TES.Domain.Accounts.Repositories;
using TES.Domain.Common;

namespace TES.Application.Accounts.Commands.CreateAccount;

public sealed class CreateAccountCommandHandler
    : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _repo;
    private readonly IPublisher _publisher;

    public CreateAccountCommandHandler(IAccountRepository repo, IPublisher publisher)
    {
        _repo = repo;
        _publisher = publisher;
    }

    public async Task<AccountDto> Handle(
        CreateAccountCommand request,
        CancellationToken ct)
    {
        var digits = new string(request.Cpf.Where(char.IsDigit).ToArray());

        if (await _repo.ExistsByCpfAsync(digits, ct))
            throw new DomainException($"Já existe uma conta com o CPF informado.");

        var account = Account.Create(request.NomeTitular, digits);

        await _repo.AddAsync(account, ct);
        await _repo.SaveChangesAsync(ct);

        // Dispara Domain Events via MediatR (desacoplado de Fraude/Cartões)
        foreach (var domainEvent in account.DomainEvents)
            await _publisher.Publish(domainEvent, ct);

        account.ClearDomainEvents();

        return account.ToDto();
    }
}
