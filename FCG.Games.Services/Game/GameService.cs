using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.ElasticDocuments;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;
using FCG.Games.Domain.Entities;
using FCG.Games.Domain.Enums;
using FCG.Games.Domain.Errors;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Domain.Interfaces.Services;

namespace FCG.Games.Services.Game;

public class GameService(IGameRepository repository, IElasticSearchRepository elasticSearch) : IGameService
{
    public async Task<Result<GameResponse>> CreateAsync(CreateGameRequest request, CancellationToken ct)
    {
        var game = new GameEntity(request.Title, request.Description, request.Genre, request.Platform, request.Developer, request.Price);
        await repository.AddAsync(game);

        var gameElastic = new GameElasticDocument
        {
            Id = game.Id.ToString(),
            Title = game.Title,
            Description = game.Description,
            Genre = game.Genre.ToString(),
            Platform = game.Platform.ToString(),
            Developer = game.Developer,
            Price = game.Price,
            CreateDate = game.CreateDate
        };

        await elasticSearch.IndexAsync(gameElastic, ct);

        Enum.TryParse<GameGenre>(game.Genre, true, out var genre);
        Enum.TryParse<GamePlatform>(game.Platform, true, out var platform);

        return Result<GameResponse>.Ok(new GameResponse(game.Id, game.Title, game.Description, genre, platform, game.Developer, game.Price));
    }

    public async Task<Result<List<GameResponse>>> ListAsync()
    {
        var games = await repository.GetAllAsync();

        return Result<List<GameResponse>>.Ok(
            games.Select(x => new GameResponse(
                x.Id,
                x.Title,
                x.Description,
                Enum.Parse<GameGenre>(x.Genre, true),
                Enum.Parse<GamePlatform>(x.Platform, true),
                x.Developer,
                x.Price
            )).ToList()
        );
    }
    public async Task<Result<List<GameResponse>>> SearchAsync(string? query, string? genre, string? platform, CancellationToken ct)
    {
        var games = await elasticSearch.SearchAsync(query, genre, platform, ct);

        return Result<List<GameResponse>>.Ok(
            games.Select(x => new GameResponse(
                Guid.Parse(x.Id),
                x.Title,
                x.Description,
                Enum.Parse<GameGenre>(x.Genre, true),
                Enum.Parse<GamePlatform>(x.Platform, true),
                x.Developer,
                x.Price
            )).ToList()
        );
    }

    public async Task<Result<GameMetricsResult>> MetricsAsync(CancellationToken ct)
    {
        var metrics = await elasticSearch.GetMetricsAsync(ct);

        return Result<GameMetricsResult>.Ok(metrics);
    }

    public async Task<Result<List<GameResponse>>> RecommendationAsync(Guid gameId, CancellationToken ct)
    {
        var game = await repository.GetByIdAsync(gameId);

        if (game == null)
            return Result<List<GameResponse>>.Fail(ErrorFactory.NotFound("Jogo não encontrado."));

        var games = await elasticSearch.GetRecommendationsAsync(game.Genre, game.Platform, game.Id.ToString(), ct);

        return Result<List<GameResponse>>.Ok(
            games.Select(x => new GameResponse(
                Guid.Parse(x.Id),
                x.Title,
                x.Description,
                Enum.Parse<GameGenre>(x.Genre, true),
                Enum.Parse<GamePlatform>(x.Platform, true),
                x.Developer,
                x.Price
            )).ToList()
        );
    }
}
