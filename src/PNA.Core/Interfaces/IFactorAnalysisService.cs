using PNA.Core.Entities;

namespace PNA.Core.Interfaces;

public interface IFactorAnalysisService
{
    Task<List<double>> PerformFactorAnalysisAsync ( List<TestResult> results );
}