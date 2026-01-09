using FCG.Games.Domain.DTOs.ElasticDocuments;
using FCG.Games.Domain.Enums;

namespace FCG.Games.Domain.Interfaces.Repositories;

public interface IElasticSearchRepository
{
    Task IndexAsync(GameElasticDocument game, CancellationToken ct);

    Task<IReadOnlyCollection<GameElasticDocument>> SearchAsync(
        string? query,
        string? genre,
        string? platform,
        CancellationToken ct
    );
    Task<GameMetricsResult> GetMetricsAsync(CancellationToken ct);
    Task<IReadOnlyCollection<GameElasticDocument>> GetRecommendationsAsync(
        string genre,
        string platform,
        string gameId,
        CancellationToken ct
    );
}
