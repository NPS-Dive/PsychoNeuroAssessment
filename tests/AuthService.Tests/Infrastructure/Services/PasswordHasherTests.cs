using FluentAssertions;
using PNA.AuthService.Infrastructure.Services;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Infrastructure.Services;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher = new();

    [Fact]
    public void HashPassword_ShouldReturnConsistentHash ()
    {
        string password = "test123";
        string hash1 = _passwordHasher.HashPassword(password);
        string hash2 = _passwordHasher.HashPassword(password);

        hash1.Should().Be(hash2);
        hash1.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrueForCorrectPassword ()
    {
        string password = "test123";
        string hash = _passwordHasher.HashPassword(password);

        bool result = _passwordHasher.VerifyPassword(password, hash);
        result.Should().BeTrue();
    }
}