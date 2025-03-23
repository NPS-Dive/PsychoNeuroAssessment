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

    public async Task AddAsync ( User user )
    {
        await _users.InsertOneAsync(user);
    }

    public async Task<User?> GetByIdAsync ( Guid id )
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByUsernameAsync ( string username )
    {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetAllAsync ()
    {
        return await _users.Find(_ => true).ToListAsync();
    }

    public async Task UpdateAsync ( User user )
    {
        await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
    }
}