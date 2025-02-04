using Application;
using Application.Commands.Orders;
using Application.DTO;
using Application.Queries.Orders;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("orders")]
[ApiController]
public class OrdersController(IDispatcher dispatcher) : ControllerBase
{
    [Authorize(Roles = "user")]
    [HttpGet("cusomerid={id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await dispatcher.DispatchQueryAsync<GetMyOrdersQuery, Result<List<OrderDTO>>>(new(id));
        return Ok(result);
    }

    [Authorize(Roles = "user")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await dispatcher.DispatchCommandAsync<DeleteOrderCommand,Result<string>>(new(id));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("confirm/id={id}")]
    public async Task<IActionResult> Confirm(Guid id)
    {
        var result = await dispatcher.DispatchCommandAsync<ConfirmOrderCommand,Result<Guid>>(new ConfirmOrderCommand(id));
        return Ok(result);
    }

    [Authorize(Roles = "user")]
    [HttpPost]
    public async Task<IActionResult> Create(AddOrderCommand addOrderCommand)
    {
        var result = await dispatcher.DispatchCommandAsync<AddOrderCommand, Result<OrderDTO>>(addOrderCommand);
        return Ok(result);
    }
}