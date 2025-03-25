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
        _users = database.GetCollection<User>("Users");
    }

    public async Task<User> AddAsync ( User entity )
    {
        await _users.InsertOneAsync(entity);
        return entity;
    }

    public async Task<User?> GetByIdAsync ( Guid id ) =>
        await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task<User?> FindByEmailAsync ( string email ) =>
        await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<User>> GetAllAsync () =>
        await _users.Find(_ => true).ToListAsync().ContinueWith(t => t.Result.AsReadOnly());

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