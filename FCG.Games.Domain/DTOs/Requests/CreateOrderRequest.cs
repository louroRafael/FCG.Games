using FCG.Games.Domain.Enums;

namespace FCG.Games.Domain.DTOs.Requests;

public record CreateOrderRequest(Guid UserId, Guid[] GameIds, PaymentMethod PaymentMethod);
