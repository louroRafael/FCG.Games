using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;
using FCG.Games.Domain.Entities;
using FCG.Games.Domain.Enums;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Domain.Interfaces.Services;

namespace FCG.Games.Services.Game;

public class GameService(IGameRepository repository) : IGameService
{
    public async Task<Result<GameResponse>> CreateAsync(CreateGameRequest request)
    {
        var game = new GameEntity(request.Title, request.Description, request.Genre, request.Platform, request.Developer, request.Price);
        await repository.AddAsync(game);

        //Add Elastic Search Index

        Enum.TryParse<GameGenre>(game.Genre, true, out var genre);
        Enum.TryParse<GamePlatform>(game.Platform, true, out var platform);

        return Result<GameResponse>.Ok(new GameResponse(game.Id, game.Title, game.Description, genre, platform, game.Developer, game.Price));
    }

    public async Task<Result<List<GameResponse>>> ListAsync()
    {
        throw new NotImplementedException();
    }
}
