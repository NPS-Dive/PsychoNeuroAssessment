using MongoDB.Driver;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.TestService.Infrastructure.Data;

public class MongoTestResultRepository : ITestResultRepository
{
    private readonly IMongoCollection<TestResult> _results;

    public MongoTestResultRepository ( IConfiguration configuration )
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDB:Database"]);
        _results = database.GetCollection<TestResult>("TestResults");
    }

    public async Task AddAsync ( TestResult result )
    {
        await _results.InsertOneAsync(result);
    }

    public async Task<TestResult?> GetByIdAsync ( Guid id )
    {
        return await _results.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<TestResult>> GetByUserIdAsync ( Guid userId )
    {
        return await _results.Find(r => r.UserId == userId).ToListAsync();
    }

    public async Task UpdateAsync ( TestResult result )
    {
        await _results.ReplaceOneAsync(r => r.Id == result.Id, result);
    }
}