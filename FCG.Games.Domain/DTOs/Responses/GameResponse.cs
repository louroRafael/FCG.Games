using FCG.Games.Domain.Enums;

namespace FCG.Games.Domain.DTOs.Responses;

public record GameResponse(Guid Id, string Title, string Description, GameGenre Genre, GamePlatform Platform, string Developer, decimal Price);
