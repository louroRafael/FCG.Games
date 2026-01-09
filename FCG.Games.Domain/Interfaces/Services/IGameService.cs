using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.ElasticDocuments;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;

namespace FCG.Games.Domain.Interfaces.Services;

public interface IGameService
{
    Task<Result<GameResponse>> CreateAsync(CreateGameRequest request, CancellationToken ct);
    Task<Result<List<GameResponse>>> ListAsync();
    Task<Result<List<GameResponse>>> SearchAsync(string? query, string? genre, string? platform, CancellationToken ct);
    Task<Result<GameMetricsResult>> MetricsAsync(CancellationToken ct);
    Task<Result<List<GameResponse>>> RecommendationAsync(Guid gameId, CancellationToken ct);
}
