using PNA.Core.Entities;

namespace PNA.Core.Interfaces;

public interface IStatisticsService
{
    Task<Dictionary<string, double>> CalculateStatisticsAsync ( List<TestResult> results );
}