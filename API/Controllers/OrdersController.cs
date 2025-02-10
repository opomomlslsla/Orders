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
    [Authorize(Roles = "admin")]
    [HttpGet("/{pageNumber}/{statusFilter}")]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string? statusFilter)
    {
        var result = await dispatcher.DispatchQueryAsync<GetOrdersQuery, Result<List<OrderDTO>>>(new GetOrdersQuery(pageNumber, pageSize, statusFilter));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles = "user,admin")]
    [HttpGet("customerid={id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await dispatcher.DispatchQueryAsync<GetMyOrdersQuery, Result<List<OrderDTO>>>(new(id));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = "user")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await dispatcher.DispatchCommandAsync<DeleteOrderCommand,Result<string>>(new(id));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("confirm/id={id}")]
    public async Task<IActionResult> Confirm(Guid id)
    {
        var result = await dispatcher.DispatchCommandAsync<ConfirmOrderCommand,Result<Guid>>(new ConfirmOrderCommand(id));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = "user")]
    [HttpPost]
    public async Task<IActionResult> Create(AddOrderCommand addOrderCommand)
    {
        var result = await dispatcher.DispatchCommandAsync<AddOrderCommand, Result<OrderDTO>>(addOrderCommand);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}