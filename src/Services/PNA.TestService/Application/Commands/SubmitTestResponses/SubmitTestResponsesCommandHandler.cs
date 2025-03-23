using MediatR;
using PNA.Core.Commands;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;

namespace PNA.TestService.Application.Commands.SubmitTestResponses;

public class SubmitTestResponsesCommandHandler : IRequestHandler<SubmitTestResponsesCommand, Unit>
{
    private readonly ITestResultRepository _testResultRepository;
    private readonly ITestCalculator _testCalculator;

    public SubmitTestResponsesCommandHandler ( ITestResultRepository testResultRepository, ITestCalculator testCalculator )
    {
        _testResultRepository = testResultRepository;
        _testCalculator = testCalculator;
    }

    public async Task<Unit> Handle ( SubmitTestResponsesCommand request, CancellationToken cancellationToken )
    {
        var score = _testCalculator.CalculateScore(request.Responses);
        var result = new TestResult(request.UserId, request.TestType, request.Responses, score, DateTime.UtcNow);
        await _testResultRepository.AddAsync(result);
        return Unit.Value;
    }
}