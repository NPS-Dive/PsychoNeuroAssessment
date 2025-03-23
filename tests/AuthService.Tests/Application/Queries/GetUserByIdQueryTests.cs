using FluentAssertions;
using Moq;
using PNA.AuthService.Application.Queries.GetUserById;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Application.Queries;

public class GetUserByIdQueryTests
{
    [Fact]
    public async Task Handle_ShouldReturnUser_WhenFound ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var user = new User("testuser", "hash", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);
        userRepoMock.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);
        var handler = new GetUserByIdQueryHandler(userRepoMock.Object);
        var query = new GetUserByIdQuery(user.Id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenNotFound ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);
        var handler = new GetUserByIdQueryHandler(userRepoMock.Object);
        var query = new GetUserByIdQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}