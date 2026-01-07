using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;

namespace FCG.Games.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync(CreateOrderRequest request, CancellationToken ct);
    Task<Result<List<OrderResponse>>> ListAsync(CancellationToken ct);
    Task<Result<OrderResponse?>> GetByIdAsync(Guid id);
    Task<Result<List<OrderResponse>>> GetByUserIdAsync(Guid userId);
}
