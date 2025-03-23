using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PNA.AuthService.Infrastructure.Data;
using PNA.AuthService.Infrastructure.Services;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Architecture;

public class DependencyInjectionTests
{
    [Fact]
    public void ServiceCollection_ShouldResolveDependencies ()
    {
        var services = new ServiceCollection();
        services.AddScoped<IUserRepository, MongoUserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IRedisCacheService, RedisCacheService>();
        var provider = services.BuildServiceProvider();

        provider.GetService<IUserRepository>().Should().NotBeNull();
        provider.GetService<IPasswordHasher>().Should().NotBeNull();
        provider.GetService<IRedisCacheService>().Should().NotBeNull();
    }
}