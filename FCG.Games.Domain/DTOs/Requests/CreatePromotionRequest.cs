namespace FCG.Games.Domain.DTOs.Requests;

public record CreatePromotionRequest(string Title, int PercentualDiscount, DateTime StartDate, DateTime EndDate, Guid GameId);
