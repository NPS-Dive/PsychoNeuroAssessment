using MongoDB.Driver;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.AuthService.Infrastructure.Data;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public MongoUserRepository ( IConfiguration configuration )
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDB:Database"]);
        _users = database.GetCollection<User>("User");
    }

    public async Task<User> AddAsync ( User entity )
    {
        await _users.InsertOneAsync(entity);
        return entity;
    }

    public async Task<User?> GetByIdAsync ( Guid id )
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> FindByEmailAsync ( string email )
    {
        return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync ( User entity )
    {
        entity.Update(entity.FirstName, entity.LastName);
        await _users.ReplaceOneAsync(u => u.Id == entity.Id, entity);
    }

    public async Task DeleteAsync ( Guid id )
    {
        await _users.DeleteOneAsync(u => u.Id == id);
    }
}