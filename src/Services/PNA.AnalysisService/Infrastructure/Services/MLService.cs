using PNA.Core.Interfaces;

namespace PNA.AnalysisService.Infrastructure.Services;

public class MLService : IMLService
{
    public Task<double> PredictOutcomeAsync ( Dictionary<string, int> responses )
    {
        // Placeholder for ML model prediction
        return Task.FromResult(responses.Values.Average() * 1.5);
    }
}