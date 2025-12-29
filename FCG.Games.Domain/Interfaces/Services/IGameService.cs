using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;

namespace FCG.Games.Domain.Interfaces.Services;

public interface IGameService
{
    Task<Result<GameResponse>> CreateAsync(CreateGameRequest request);
    Task<Result<List<GameResponse>>> ListAsync();
}
