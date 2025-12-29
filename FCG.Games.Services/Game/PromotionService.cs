using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Domain.Interfaces.Services;

namespace FCG.Games.Services.Game;

public class PromotionService(IPromotionRepository repository) : IPromotionService
{
    public async Task<Result<PromotionResponse>> CreateAsync(CreatePromotionRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<PromotionResponse>>> ListAsync()
    {
        throw new NotImplementedException();
    }
}
