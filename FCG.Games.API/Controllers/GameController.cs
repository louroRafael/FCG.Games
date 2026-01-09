using FCG.Games.API.Extensions;
using FCG.Games.API.Filters;
using FCG.Games.Domain.DTOs.Requests;
using FCG.Games.Domain.Enums;
using FCG.Games.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Games.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GameController(IGameService service) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ValidationFilter<CreateGameRequest>))]
    public async Task<IActionResult> Create([FromBody] CreateGameRequest request, CancellationToken ct)
    {
        var gameCreated = await service.CreateAsync(request, ct);
        return gameCreated.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var games = await service.ListAsync();
        return games.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string? query,
        [FromQuery] GameGenre? genre,
        [FromQuery] GamePlatform? platform, CancellationToken ct)
    {
        var games = await service.SearchAsync(query, genre.ToString(), platform.ToString(), ct);
        return games.ToActionResult();
    }

    [HttpGet("{id}/recommendations")]
    public async Task<IActionResult> Recommendations(Guid id, CancellationToken ct)
    {
        var games = await service.RecommendationAsync(id, ct);
        return games.ToActionResult();
    }

    [HttpGet("metrics")]
    public async Task<IActionResult> Metrics(CancellationToken ct)
    {
        var metrics = await service.MetricsAsync(ct);
        return metrics.ToActionResult();
    }
}
