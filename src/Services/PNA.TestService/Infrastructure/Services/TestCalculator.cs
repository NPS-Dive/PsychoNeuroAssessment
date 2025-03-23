using PNA.Core.Interfaces;

namespace PNA.TestService.Infrastructure.Services;

public class TestCalculator : ITestCalculator
{
    public double CalculateScore ( Dictionary<string, int> responses )
    {
        return responses.Values.Average(); // Simplified scoring logic
    }
}