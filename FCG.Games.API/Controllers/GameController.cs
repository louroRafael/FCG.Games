using FCG.Games.API.Extensions;
using FCG.Games.API.Filters;
using FCG.Games.Domain.DTOs.Requests;
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
    public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
    {
        var gameCreated = await service.CreateAsync(request);
        return gameCreated.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var games = await service.ListAsync();
        return games.ToActionResult();
    }
}
