using FCG.Games.Domain.Enums;

namespace FCG.Games.Domain.DTOs.Requests;

public record SearchGameRequest(string searchText, GameGenre Genre, GamePlatform Platform);
