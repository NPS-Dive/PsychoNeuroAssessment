using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.AnalysisService.Infrastructure.Services;

public class StatisticsService : IStatisticsService
{
    public Task<Dictionary<string, double>> CalculateStatisticsAsync ( List<TestResult> results )
    {
        var stats = new Dictionary<string, double>
        {
            { "AverageScore", results.Any() ? results.Average(r => r.Score) : 0 },
            { "Count", results.Count }
        };
        return Task.FromResult(stats);
    }
}