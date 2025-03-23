using FluentAssertions;
using Moq;
using PNA.AnalysisService.Application.Queries.GetStatistics;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;

namespace AnalysisService.Tests.Application.Queries;

public class GetStatisticsQueryTests
{
    [Fact]
    public async Task Handle_ShouldReturnStatistics ()
    {
        var statsServiceMock = new Mock<IStatisticsService>();
        var stats = new Dictionary<string, double> { { "AverageScore", 75.0 } };
        statsServiceMock.Setup(x => x.CalculateStatisticsAsync(It.IsAny<List<TestResult>>())).ReturnsAsync(stats);
        var handler = new GetStatisticsQueryHandler(statsServiceMock.Object);
        var query = new GetStatisticsQuery(new List<TestResult> { new(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 75.0, DateTime.UtcNow) });

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(stats);
    }
}