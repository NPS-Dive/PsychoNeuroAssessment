using FluentAssertions;
using PNA.AnalysisService.Infrastructure.Services;
using PNA.Core.Entities;
using PNA.Core.Enums;

namespace AnalysisService.Tests.Infrastructure.Services;

public class StatisticsServiceTests
{
    private readonly StatisticsService _statsService = new();

    [Fact]
    public async Task CalculateStatisticsAsync_ShouldReturnCorrectStats ()
    {
        var results = new List<TestResult>
        {
            new(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 50.0, DateTime.UtcNow),
            new(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 100.0, DateTime.UtcNow)
        };

        var stats = await _statsService.CalculateStatisticsAsync(results);

        stats["AverageScore"].Should().Be(75.0);
        stats["Count"].Should().Be(2.0);
    }
}