using FCG.Games.API.Extensions;
using FCG.Games.API.Filters;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Games.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class PromotionController(IPromotionService service) : ControllerBase
{
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<CreatePromotionRequest>))]
    public async Task<IActionResult> Create([FromBody] CreatePromotionRequest request)
    {
        var promotionCreated = await service.CreateAsync(request);
        return promotionCreated.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var promotions = await service.ListAsync();
        return promotions.ToActionResult();
    }
}
