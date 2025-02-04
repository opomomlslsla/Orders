using Application;
using Application.Commands.Carts;
using Application.DTO;
using Application.Queries.Carts;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("carts")]
[ApiController]
public class CartsController(IDispatcher dispatcher) : ControllerBase
{
    [Authorize(Roles = "user")]
    [HttpGet("customerid={id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await dispatcher.DispatchQueryAsync<GetMyCartQuery,Result<CartDTO>>(new (id));
        return Ok(result);
    }

    [Authorize(Roles = "user")]
    [HttpPost]
    public async Task<IActionResult> AddToCart(AddToCartCommand command)
    {
        var result = await dispatcher.DispatchCommandAsync<AddToCartCommand, Result<string>>(command);
        return Ok(result);
    }

    [Authorize(Roles = "user")]
    [HttpDelete]
    public async Task<IActionResult> RemoveFromCart(RemoveFromCartCommand command)
    {
        var result = await dispatcher.DispatchCommandAsync<RemoveFromCartCommand, Result<string>>(command);
        return Ok(result);
    }
}