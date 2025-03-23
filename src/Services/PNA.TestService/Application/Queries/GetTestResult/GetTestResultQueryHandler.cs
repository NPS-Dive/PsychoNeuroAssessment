using MediatR;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.TestService.Application.Queries.GetTestResult;

public class GetTestResultQueryHandler : IRequestHandler<GetTestResultQuery, TestResult?>
{
    private readonly ITestResultRepository _testResultRepository;

    public GetTestResultQueryHandler ( ITestResultRepository testResultRepository )
    {
        _testResultRepository = testResultRepository;
    }

    public async Task<TestResult?> Handle ( GetTestResultQuery request, CancellationToken cancellationToken )
    {
        return await _testResultRepository.GetByIdAsync(request.Id);
    }
}