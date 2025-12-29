using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;

namespace FCG.Games.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync(CreateOrderRequest request);
    Task<Result<List<OrderResponse>>> ListAsync();
    Task<Result<OrderResponse?>> GetByIdAsync(int id);
    Task<Result<List<OrderResponse>>> GetByUserIdAsync(int userId);
}
