using Microsoft.EntityFrameworkCore;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.AuthService.Infrastructure.Data;

public class SqlUserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public SqlUserRepository ( AuthDbContext context )
    {
        _context = context;
    }

    public async Task<User> AddAsync ( User entity )
    {
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<User?> GetByIdAsync ( Guid id ) =>
        await _context.Users.FindAsync(id);

    public async Task<User?> FindByEmailAsync ( string email ) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task UpdateAsync ( User entity )
    {
        entity.Update(entity.FirstName, entity.LastName);
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync ( Guid id )
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}