using PNA.Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace PNA.AuthService.Infrastructure.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _database;

    public RedisCacheService ( IConnectionMultiplexer redis )
    {
        _database = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T> ( string key )
    {
        var value = await _database.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value) : default;
    }

    public async Task SetAsync<T> ( string key, T value, TimeSpan? expiry = null )
    {
        await _database.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
    }
}