namespace FCG.Games.Domain.DTOs.Responses;

public record PromotionResponse(Guid Id, string Title, int PercentualDiscount, DateTime StartDate, DateTime EndDate, Guid GameId);
