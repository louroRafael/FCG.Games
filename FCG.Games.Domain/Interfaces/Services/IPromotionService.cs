using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;

namespace FCG.Games.Domain.Interfaces.Services;

public interface IPromotionService
{
    Task<Result<PromotionResponse>> CreateAsync(CreatePromotionRequest request);
    Task<Result<List<PromotionResponse>>> ListAsync();
}
