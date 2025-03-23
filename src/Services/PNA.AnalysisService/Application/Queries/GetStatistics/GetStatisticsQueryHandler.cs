using MediatR;
using PNA.Core.Interfaces;

namespace PNA.AnalysisService.Application.Queries.GetStatistics;

public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, Dictionary<string, double>>
{
    private readonly IStatisticsService _statisticsService;

    public GetStatisticsQueryHandler ( IStatisticsService statisticsService )
    {
        _statisticsService = statisticsService;
    }

    public async Task<Dictionary<string, double>> Handle ( GetStatisticsQuery request, CancellationToken cancellationToken )
    {
        return await _statisticsService.CalculateStatisticsAsync(request.Results);
    }
}