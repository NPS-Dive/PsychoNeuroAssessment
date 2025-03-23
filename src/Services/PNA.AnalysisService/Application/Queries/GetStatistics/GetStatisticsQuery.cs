using MediatR;
using PNA.Core.Entities;

namespace PNA.AnalysisService.Application.Queries.GetStatistics;

public record GetStatisticsQuery (
    List<TestResult> Results )
    : IRequest<Dictionary<string, double>>;