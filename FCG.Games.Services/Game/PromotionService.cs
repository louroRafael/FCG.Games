using FCG.Games.Domain.Common;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.DTOs.Responses;
using FCG.Games.Domain.Entities;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Domain.Interfaces.Services;

namespace FCG.Games.Services.Game;

public class PromotionService(IPromotionRepository repository) : IPromotionService
{
    public async Task<Result<PromotionResponse>> CreateAsync(CreatePromotionRequest request)
    {
        var promotion = new PromotionEntity(request.Title, request.PercentualDiscount, request.StartDate, request.EndDate, request.GameId);
        await repository.AddAsync(promotion);

        return Result<PromotionResponse>.Ok(new PromotionResponse(promotion.Id, promotion.Title, promotion.PercentualDiscount, promotion.StartDate, promotion.EndDate, promotion.GameId));
    }

    public async Task<Result<List<PromotionResponse>>> ListAsync()
    {
        var promotions = await repository.GetAllAsync();

        return Result<List<PromotionResponse>>.Ok(
            promotions.Select(x => new PromotionResponse(
                x.Id,
                x.Title,
                x.PercentualDiscount,
                x.StartDate,
                x.EndDate,
                x.GameId
            )).ToList()
        );
    }
}
