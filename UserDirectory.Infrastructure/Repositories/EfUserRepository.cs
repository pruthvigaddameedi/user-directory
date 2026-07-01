using Microsoft.EntityFrameworkCore;
using UserDirectory.Application.Interfaces;
using UserDirectory.Domain.Entities;
using UserDirectory.Infrastructure.Data;

namespace UserDirectory.Infrastructure.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public EfUserRepository(AppDbContext db) => _db = db;

    public async Task<List<User>> GetAllAsync(CancellationToken ct = default) =>
        await _db.Users.AsNoTracking().ToListAsync(ct);

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.Users.FindAsync(new object[] { id }, ct);

    public async Task<User> AddAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return user;
    }

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var u = await _db.Users.FindAsync(new object[] { id }, ct);
        if (u != null) { _db.Users.Remove(u); await _db.SaveChangesAsync(ct); }
    }
}
