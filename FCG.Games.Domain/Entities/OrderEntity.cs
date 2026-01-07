using FCG.Games.Domain.Enums;
using FCG.Games.Domain.Interfaces.Repositories;

namespace FCG.Games.Domain.Entities;

public class OrderEntity : EntityBase, IAggregateRoot
{
    public Guid UserId { get; set; }
    public string OrderNumber { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }

    public ICollection<OrderItemEntity> Items { get; set; } = [];

    public OrderEntity() { }

    public OrderEntity(Guid userId, IEnumerable<Guid> gamesIds, decimal totalAmount, PaymentMethod paymentMethod)
    {
        UserId = userId;
        Status = OrderStatus.InProcess.ToString();
        TotalAmount = totalAmount;
        PaymentMethod = paymentMethod.ToString();

        var date = DateTime.UtcNow.ToString("yyyyMMdd");
        var guid = Guid.NewGuid().ToString("N")[..6].ToUpper();

        OrderNumber = $"ORD-{date}-{guid}";

        foreach (var gameId in gamesIds)
        {
            Items.Add(new OrderItemEntity(Id, gameId));
        }
    }
}
