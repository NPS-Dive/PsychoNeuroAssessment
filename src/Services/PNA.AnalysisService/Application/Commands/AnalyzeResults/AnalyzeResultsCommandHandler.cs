using MediatR;
using PNA.Core.Interfaces;

namespace PNA.AnalysisService.Application.Commands.AnalyzeResults;

public class AnalyzeResultsCommandHandler : IRequestHandler<AnalyzeResultsCommand, double>
{
    private readonly IMLService _mlService;

    public AnalyzeResultsCommandHandler ( IMLService mlService )
    {
        _mlService = mlService;
    }

    public async Task<double> Handle ( AnalyzeResultsCommand request, CancellationToken cancellationToken )
    {
        return await _mlService.PredictOutcomeAsync(request.Responses.First());
    }
}