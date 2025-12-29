namespace FCG.Games.Domain.Entities;

public class OrderItemEntity : EntityBase
{
    public Guid OrderId { get; private set; }
    public Guid GameId { get; private set; }

    public virtual OrderEntity Order { get; set; }
    public virtual GameEntity Game { get; private set; }

    public OrderItemEntity() { }

    public OrderItemEntity(Guid orderId, Guid gameId)
    {
        OrderId = orderId;
        GameId = gameId;
    }
}
