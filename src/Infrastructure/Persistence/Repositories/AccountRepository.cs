using Microsoft.EntityFrameworkCore;
using TES.Domain.Accounts.Entities;
using TES.Domain.Accounts.Repositories;
using TES.Infrastructure.Persistence.Context;

namespace TES.Infrastructure.Persistence.Repositories;

public sealed class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context) => _context = context;

    public Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken ct = default) =>
        _context.Accounts.OrderBy(a => a.NomeTitular).ToListAsync(ct)
            .ContinueWith(t => (IReadOnlyList<Account>)t.Result, ct);

    public Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _context.Accounts.FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct = default) =>
        _context.Accounts.AnyAsync(a => a.Cpf.Value == cpf, ct);

    public Task AddAsync(Account account, CancellationToken ct = default)
    {
        _context.Accounts.Add(account);
        return Task.CompletedTask;
    }

    public void Update(Account account) => _context.Accounts.Update(account);

    public void Remove(Account account) => _context.Accounts.Remove(account);

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        _context.SaveChangesAsync(ct);
}
