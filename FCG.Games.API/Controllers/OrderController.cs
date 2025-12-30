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
public class OrderController(IOrderService service) : ControllerBase
{
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<CreateOrderRequest>))]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var orderCreated = await service.CreateAsync(request);
        return orderCreated.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var orders = await service.ListAsync();
        return orders.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await service.GetByIdAsync(id);
        return order.ToActionResult();
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var orders = await service.GetByUserIdAsync(userId);
        return orders.ToActionResult();
    }
}
