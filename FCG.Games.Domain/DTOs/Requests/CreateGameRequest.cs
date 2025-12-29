using FCG.Games.Domain.Enums;

namespace FCG.Games.Domain.DTOs.Requests;

public record CreateGameRequest(string Title, string Description, GameGenre Genre, GamePlatform Platform, string Developer, decimal Price);
