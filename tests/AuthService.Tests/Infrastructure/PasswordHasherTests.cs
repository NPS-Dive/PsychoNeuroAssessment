using PNA.AuthService.Infrastructure.Services;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Infrastructure;

public class PasswordHasherTests
{
    private readonly IPasswordHasher _passwordHasher = new PasswordHasher();

    [Fact]
    public void HashPassword_ReturnsConsistentHash ()
    {
        string password = "test123";
        string hash1 = _passwordHasher.HashPassword(password);
        string hash2 = _passwordHasher.HashPassword(password);

        Assert.Equal(hash1, hash2);
        Assert.NotNull(hash1);
        Assert.NotEmpty(hash1);
    }

    [Fact]
    public void VerifyPassword_ReturnsTrueForCorrectPassword ()
    {
        string password = "test123";
        string hash = _passwordHasher.HashPassword(password);

        bool result = _passwordHasher.VerifyPassword(password, hash);
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_ReturnsFalseForIncorrectPassword ()
    {
        string password = "test123";
        string wrongPassword = "wrong456";
        string hash = _passwordHasher.HashPassword(password);

        bool result = _passwordHasher.VerifyPassword(wrongPassword, hash);
        Assert.False(result);
    }
}