using FluentAssertions;
using Moq;
using PNA.AuthService.Infrastructure.Services;
using StackExchange.Redis;

namespace AuthService.Tests.Infrastructure.Services;

public class RedisCacheServiceTests
{
    private readonly Mock<IDatabase> _databaseMock;
    private readonly RedisCacheService _redisCacheService;

    public RedisCacheServiceTests ()
    {
        var redisMock = new Mock<IConnectionMultiplexer>();
        _databaseMock = new Mock<IDatabase>();
        redisMock.Setup(x => x.GetDatabase(-1, null)).Returns(_databaseMock.Object);
        _redisCacheService = new RedisCacheService(redisMock.Object);
    }

    [Fact]
    public async Task SetAsync_ShouldCallStringSet ()
    {
        // Arrange
        var key = "testkey";
        var value = "testvalue";

        // Act
        await _redisCacheService.SetAsync(key, value);

        // Assert
        _databaseMock.Verify(x => x.StringSetAsync(
            key,
            value,          // Match the exact value passed to StringSetAsync
            null,           // expiry (TimeSpan?)
            (bool)(bool?)null,    // keepTtl (explicitly cast null to bool?)
            When.Always,    // when (enum, default is When.Always)
            CommandFlags.None), Times.Once());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnDeserializedValue ()
    {
        // Arrange
        var key = "testkey";
        _databaseMock.Setup(x => x.StringGetAsync(key, CommandFlags.None)).ReturnsAsync("\"testvalue\"");

        // Act
        var result = await _redisCacheService.GetAsync<string>(key);

        // Assert
        result.Should().Be("testvalue");
    }
}