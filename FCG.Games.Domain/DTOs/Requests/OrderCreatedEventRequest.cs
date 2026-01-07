using FCG.Games.Domain.Enums;

namespace FCG.Games.Domain.DTOs.Requests;

public record OrderCreatedEventRequest(Guid OrderId, decimal TotalAmount, PaymentMethod PaymentMethod);
