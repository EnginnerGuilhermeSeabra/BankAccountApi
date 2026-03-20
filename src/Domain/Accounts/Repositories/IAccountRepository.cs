using TES.Domain.Accounts.Entities;

namespace TES.Domain.Accounts.Repositories;

public interface IAccountRepository
{
    Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken ct = default);
    Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct = default);
    Task AddAsync(Account account, CancellationToken ct = default);
    void Update(Account account);
    void Remove(Account account);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
