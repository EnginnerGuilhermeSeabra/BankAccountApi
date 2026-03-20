using MediatR;
using TES.Application.Accounts.DTOs;
using TES.Application.Common.Interfaces;
using TES.Domain.Accounts.Entities;
using TES.Domain.Accounts.Repositories;
using TES.Domain.Common;

namespace TES.Application.Accounts.Commands.UpdateAccount;

public sealed class UpdateAccountCommandHandler
    : IRequestHandler<UpdateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _repo;
    private readonly IPublisher _publisher;
    private readonly IAccountCacheService _cache;

    public UpdateAccountCommandHandler(
        IAccountRepository repo,
        IPublisher publisher,
        IAccountCacheService cache)
    {
        _repo = repo;
        _publisher = publisher;
        _cache = cache;
    }

    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken ct)
    {
        var account = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new NotFoundException(nameof(Account), request.Id);

        account.Atualizar(request.NomeTitular, request.Status);
        _repo.Update(account);
        await _repo.SaveChangesAsync(ct);

        // Invalida cache para forçar re-leitura atualizada
        _cache.Invalidate(request.Id);

        foreach (var domainEvent in account.DomainEvents)
            await _publisher.Publish(domainEvent, ct);

        account.ClearDomainEvents();

        return account.ToDto();
    }
}
