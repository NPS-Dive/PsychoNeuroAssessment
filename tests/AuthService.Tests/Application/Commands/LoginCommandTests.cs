using FluentAssertions;
using Moq;
using PNA.AuthService.Application.Commands.Login;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Application.Commands;

public class LoginCommandTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenPasswordMatches ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        userRepoMock.Setup(x => x.GetByUsernameAsync("testuser"))
            .ReturnsAsync(new User("testuser", "hashedpassword", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null));
        passwordHasherMock.Setup(x => x.VerifyPassword("password", "hashedpassword")).Returns(true);
        var handler = new LoginCommandHandler(userRepoMock.Object, passwordHasherMock.Object);
        var command = new LoginCommand("testuser", "password");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenUserNotFound ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        userRepoMock.Setup(x => x.GetByUsernameAsync("testuser")).ReturnsAsync((User?)null);
        var handler = new LoginCommandHandler(userRepoMock.Object, passwordHasherMock.Object);
        var command = new LoginCommand("testuser", "password");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }
}