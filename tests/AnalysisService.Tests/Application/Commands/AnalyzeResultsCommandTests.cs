using FluentAssertions;
using Moq;
using PNA.AnalysisService.Application.Commands.AnalyzeResults;
using PNA.Core.Interfaces;

namespace AnalysisService.Tests.Application.Commands;

public class AnalyzeResultsCommandTests
{
    [Fact]
    public async Task Handle_ShouldReturnPrediction ()
    {
        var mlServiceMock = new Mock<IMLService>();
        mlServiceMock.Setup(x => x.PredictOutcomeAsync(It.IsAny<Dictionary<string, int>>())).ReturnsAsync(75.0);
        var handler = new AnalyzeResultsCommandHandler(mlServiceMock.Object);
        var command = new AnalyzeResultsCommand(new List<Dictionary<string, int>> { new() { { "q1", 5 } } });

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().Be(75.0);
    }
}