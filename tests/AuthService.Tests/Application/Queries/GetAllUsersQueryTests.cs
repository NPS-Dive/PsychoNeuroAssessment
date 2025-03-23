using FluentAssertions;
using Moq;
using PNA.AuthService.Application.Queries.GetAllUsers;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;

namespace AuthService.Tests.Application.Queries;

public class GetAllUsersQueryTests
{
    [Fact]
    public async Task Handle_ShouldReturnAllUsers ()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        var users = new List<User> { new User("testuser", "hash", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null) };
        userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
        var handler = new GetAllUsersQueryHandler(userRepoMock.Object);
        var query = new GetAllUsersQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(users);
    }
}