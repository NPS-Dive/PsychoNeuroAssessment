using MediatR;
using PNA.Core.Entities;

namespace PNA.TestService.Application.Queries.GetTestResult;

public record GetTestResultQuery (
    Guid Id )
    : IRequest<TestResult?>;