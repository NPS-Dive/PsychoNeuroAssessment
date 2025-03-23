using PNA.Core.Commands;

namespace PNA.AnalysisService.Application.Commands.AnalyzeResults;

public record AnalyzeResultsCommand (
    List<Dictionary<string, int>> Responses )
    : BaseCommand<double>;