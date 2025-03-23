namespace PNA.Core.Interfaces;

public interface IMLService
    {
    Task<double> PredictOutcomeAsync ( Dictionary<string, int> responses );
    }