using FluentAssertions;
using PNA.TestService.Infrastructure.Services;

namespace TestService.Tests.Infrastructure.Services;

public class TestCalculatorTests
{
    private readonly TestCalculator _calculator = new();

    [Fact]
    public void CalculateScore_ShouldReturnAverage ()
    {
        var responses = new Dictionary<string, int> { { "q1", 5 }, { "q2", 10 } };

        var score = _calculator.CalculateScore(responses);

        score.Should().Be(7.5);
    }
}