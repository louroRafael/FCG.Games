using FCG.Games.Domain.Enums;

namespace FCG.Games.Domain.DTOs.Responses;

public record OrderResponse(Guid Id, Guid UserId, List<OrderItemResponse> Items, PaymentMethod PaymentMethod);