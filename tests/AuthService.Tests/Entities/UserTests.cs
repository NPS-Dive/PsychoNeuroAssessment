using PNA.Core.Entities;
using PNA.Core.Enums;
using Xunit;
using FluentAssertions;

namespace AuthService.Tests.Entities;

public class UserTests
{
    [Fact]
    public void User_ShouldHaveGuidId_WhenCreated ()
    {
        var user = new User("testuser", "hash", "test@example.com", DateTime.Now.AddYears(-30),
            Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);

        user.Id.Should().NotBeEmpty();
        user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}