using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Domain.Interfaces.Services;

namespace FCG.Games.Services.Game;

public class OrderService(IOrderRepository repository) : IOrderService
{
    public Task<Result<OrderResponse>> CreateAsync(CreateOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderResponse?>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<OrderResponse>>> GetByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<OrderResponse>>> ListAsync()
    {
        throw new NotImplementedException();
    }
}
