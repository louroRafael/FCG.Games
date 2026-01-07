using Azure.Messaging.ServiceBus;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.Interfaces.Common;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FCG.Games.Infra.Publishers;

public class OrderEventPublisher : IOrderEventPublisher
{
    private readonly ServiceBusSender _sender;

    public OrderEventPublisher(IConfiguration configuration)
    {
        var client = new ServiceBusClient(
           configuration["ServiceBus:ConnectionString"]
       );

        _sender = client.CreateSender(
            configuration["ServiceBus:QueueName"]
        );
    }

    public async Task PublishOrderCreatedAsync(OrderCreatedEventRequest order, CancellationToken ct)
    {
        var body = JsonSerializer.Serialize(order);

        var message = new ServiceBusMessage(body)
        {
            ContentType = "application/json",
            Subject = "OrderCreated",
            MessageId = order.OrderId.ToString()
        };

        await _sender.SendMessageAsync(message, ct);
    }
}
