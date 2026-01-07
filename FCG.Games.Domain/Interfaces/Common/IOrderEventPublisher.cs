using FCG.Games.Domain.DTOs.Requests;

namespace FCG.Games.Domain.Interfaces.Common;

public interface IOrderEventPublisher
{
    Task PublishOrderCreatedAsync(OrderCreatedEventRequest order, CancellationToken ct);
}
