using FluentAssertions;
using PNA.AnalysisService.Infrastructure.Services;

namespace AnalysisService.Tests.Infrastructure.Services;

public class MLServiceTests
{
    private readonly MLService _mlService = new();

    [Fact]
    public async Task PredictOutcomeAsync_ShouldReturnScaledAverage ()
    {
        var responses = new Dictionary<string, int> { { "q1", 5 }, { "q2", 10 } };

        var result = await _mlService.PredictOutcomeAsync(responses);

        result.Should().Be(7.5 * 1.5);
    }
}