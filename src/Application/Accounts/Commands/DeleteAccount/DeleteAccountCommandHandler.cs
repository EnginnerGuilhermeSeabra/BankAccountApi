using MediatR;
using TES.Application.Common.Interfaces;
using TES.Domain.Accounts.Entities;
using TES.Domain.Accounts.Repositories;
using TES.Domain.Common;

namespace TES.Application.Accounts.Commands.DeleteAccount;

public sealed class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountRepository _repo;
    private readonly IPublisher _publisher;
    private readonly IAccountCacheService _cache;

    public DeleteAccountCommandHandler(
        IAccountRepository repo,
        IPublisher publisher,
        IAccountCacheService cache)
    {
        _repo = repo;
        _publisher = publisher;
        _cache = cache;
    }

    public async Task Handle(DeleteAccountCommand request, CancellationToken ct)
    {
        var account = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new NotFoundException(nameof(Account), request.Id);

        account.Inativar();
        _repo.Update(account);
        await _repo.SaveChangesAsync(ct);

        _cache.Invalidate(request.Id);

        foreach (var domainEvent in account.DomainEvents)
            await _publisher.Publish(domainEvent, ct);

        account.ClearDomainEvents();
    }
}
