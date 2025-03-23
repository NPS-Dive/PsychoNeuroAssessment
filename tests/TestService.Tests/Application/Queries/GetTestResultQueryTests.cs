using FluentAssertions;
using Moq;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;
using PNA.TestService.Application.Queries.GetTestResult;

namespace TestService.Tests.Application.Queries;

public class GetTestResultQueryTests
{
    [Fact]
    public async Task Handle_ShouldReturnTestResult ()
    {
        var testResultRepoMock = new Mock<ITestResultRepository>();
        var testResult = new TestResult(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 75.0, DateTime.UtcNow);
        testResultRepoMock.Setup(x => x.GetByIdAsync(testResult.Id)).ReturnsAsync(testResult);
        var handler = new GetTestResultQueryHandler(testResultRepoMock.Object);
        var query = new GetTestResultQuery(testResult.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(testResult);
    }
}