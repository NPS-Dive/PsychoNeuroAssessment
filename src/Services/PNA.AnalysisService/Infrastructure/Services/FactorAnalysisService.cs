using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.AnalysisService.Infrastructure.Services;

public class FactorAnalysisService : IFactorAnalysisService
{
    public Task<List<double>> PerformFactorAnalysisAsync ( List<TestResult> results )
    {
        // Placeholder for factor analysis
        var factors = results.Select(r => r.Score).ToList();
        return Task.FromResult(factors);
    }
}