using Moq;
using PNA.AuthService.Application.Commands.Register;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Application.Commands;

public class RegisterCommandTests
{
    [Fact]
    public async Task Handle_ShouldCallAddAsync_WithNewUser ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        passwordHasherMock.Setup(x => x.HashPassword("password")).Returns("hashedpassword");
        var handler = new RegisterCommandHandler(userRepoMock.Object, passwordHasherMock.Object);
        var command = new RegisterCommand("testuser", "password", "test@example.com");

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        userRepoMock.Verify(x => x.AddAsync(It.Is<User>(u =>
            u.Username == "testuser" &&
            u.PasswordHash == "hashedpassword" &&
            u.Email == "test@example.com")), Times.Once);
    }
}