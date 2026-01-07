using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;
using FCG.Games.Domain.Entities;
using FCG.Games.Domain.Enums;
using FCG.Games.Domain.Errors;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Domain.Interfaces.Services;

namespace FCG.Games.Services.Order;

public class OrderService(IOrderRepository repository, IGameRepository gameRepository) : IOrderService
{
    public async Task<Result<OrderResponse>> CreateAsync(CreateOrderRequest request)
    {
        decimal totalAmount = 0;

        if (request.GameIds == null || request.GameIds.Length == 0)
            return Result<OrderResponse>.Fail(ErrorFactory.Validation("Nenhum jogo selecionado"));

        foreach (var gameId in request.GameIds)
        {
            var game = await gameRepository.GetByIdAsync(gameId);
            if (game == null)
                return Result<OrderResponse>.Fail(ErrorFactory.NotFound("Jogo não encontrado."));

            totalAmount += game.Price;
        }

        var order = new OrderEntity(request.UserId, request.GameIds, totalAmount, request.PaymentMethod);

        await repository.AddAsync(order);

        Enum.TryParse<PaymentMethod>(order.PaymentMethod, true, out var paymentMethod);
        var orderItems = order.Items.Select(x => new OrderItemResponse(x.Id, x.GameId, x.OrderId)).ToList();

        return Result<OrderResponse>.Ok(new OrderResponse(order.Id, order.UserId, orderItems, paymentMethod));

    }

    public async Task<Result<OrderResponse>> GetByIdAsync(Guid id)
    {
        var order = await repository.GetByIdAsync(id);

        if(order == null)
            return Result<OrderResponse>.Fail(ErrorFactory.NotFound("Pedido não encontrado."));

        Enum.TryParse<PaymentMethod>(order.PaymentMethod, true, out var paymentMethod);
        var orderItems = order.Items.Select(x => new OrderItemResponse(x.Id, x.GameId, x.OrderId)).ToList();

        return Result<OrderResponse>.Ok(new OrderResponse(order.Id, order.UserId, orderItems, paymentMethod));
    }

    public async Task<Result<List<OrderResponse>>> GetByUserIdAsync(Guid userId)
    {
        var orders = await repository.GetByUserIdAsync(userId);

        if (orders == null || orders.Count == 0)
            return Result<List<OrderResponse>>.Fail(ErrorFactory.NotFound("Este usuário não possui pedidos."));

        return Result<List<OrderResponse>>.Ok(
            orders.Select(x => new OrderResponse(
                x.Id, 
                x.UserId,
                x.Items.Select(x => new OrderItemResponse(x.Id, x.GameId, x.OrderId)).ToList(),
                Enum.Parse<PaymentMethod>(x.PaymentMethod, true)
            )).ToList()
        );
    }

    public async Task<Result<List<OrderResponse>>> ListAsync(CancellationToken ct)
    {
        var orders = await repository.GetAllWithItems(ct);

        if (orders == null || orders.Count == 0)
            return Result<List<OrderResponse>>.Fail(ErrorFactory.NotFound("Não existe nenhum pedido."));

        return Result<List<OrderResponse>>.Ok(
            orders.Select(x => new OrderResponse(
                x.Id,
                x.UserId,
                x.Items.Select(x => new OrderItemResponse(x.Id, x.GameId, x.OrderId)).ToList(),
                Enum.Parse<PaymentMethod>(x.PaymentMethod, true)
            )).ToList()
        );
    }
}
