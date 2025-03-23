using Moq;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;
using PNA.TestService.Application.Commands.SubmitTestResponses;

namespace TestService.Tests.Application.Commands;

public class SubmitTestResponsesCommandTests
{
    [Fact]
    public async Task Handle_ShouldAddTestResult ()
    {
        var testResultRepoMock = new Mock<ITestResultRepository>();
        var testCalculatorMock = new Mock<ITestCalculator>();
        testCalculatorMock.Setup(x => x.CalculateScore(It.IsAny<Dictionary<string, int>>())).Returns(75.0);
        var handler = new SubmitTestResponsesCommandHandler(testResultRepoMock.Object, testCalculatorMock.Object);
        var command = new SubmitTestResponsesCommand(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int> { { "q1", 5 } });

        await handler.Handle(command, CancellationToken.None);

        testResultRepoMock.Verify(x => x.AddAsync(It.Is<TestResult>(tr =>
            tr.UserId == command.UserId &&
            tr.TestType == command.TestType &&
            tr.Score == 75.0)), Times.Once);
    }
}