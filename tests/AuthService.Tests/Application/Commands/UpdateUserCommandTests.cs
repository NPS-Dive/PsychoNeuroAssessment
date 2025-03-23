using Moq;
using PNA.AuthService.Application.Commands.UpdateUser;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Application.Commands;

public class UpdateUserCommandTests
{
    [Fact]
    public async Task Handle_ShouldUpdateUser_WhenUserExists ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var user = new User("testuser", "oldhash", "old@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);
        userRepoMock.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);
        passwordHasherMock.Setup(x => x.HashPassword("newpassword")).Returns("newhash");
        var handler = new UpdateUserCommandHandler(userRepoMock.Object, passwordHasherMock.Object);
        var command = new UpdateUserCommand(user.Id, "newuser", "newpassword", "new@example.com");

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        userRepoMock.Verify(x => x.UpdateAsync(It.Is<User>(u =>
            u.Username == "newuser" &&
            u.PasswordHash == "newhash" &&
            u.Email == "new@example.com" &&
            u.UpdatedAt.HasValue)), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUserNotFound ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        userRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);
        var handler = new UpdateUserCommandHandler(userRepoMock.Object, passwordHasherMock.Object);
        var command = new UpdateUserCommand(Guid.NewGuid(), "newuser", "newpassword", "new@example.com");

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }
}