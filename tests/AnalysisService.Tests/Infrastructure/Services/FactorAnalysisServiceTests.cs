using FluentAssertions;
using PNA.AnalysisService.Infrastructure.Services;
using PNA.Core.Entities;
using PNA.Core.Enums;

namespace AnalysisService.Tests.Infrastructure.Services;

public class FactorAnalysisServiceTests
{
    private readonly FactorAnalysisService _factorService = new();

    [Fact]
    public async Task PerformFactorAnalysisAsync_ShouldReturnScores ()
    {
        var results = new List<TestResult>
        {
            new(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 50.0, DateTime.UtcNow),
            new(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 100.0, DateTime.UtcNow)
        };

        var factors = await _factorService.PerformFactorAnalysisAsync(results);

        factors.Should().BeEquivalentTo(new List<double> { 50.0, 100.0 });
    }
}