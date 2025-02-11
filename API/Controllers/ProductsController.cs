using Application;
using Application.Commands.Products;
using Application.DTO;
using Application.Queries.Products;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("products")]
[ApiController]
public class ProductsController(IDispatcher dispatcher) : ControllerBase
{
    [Authorize(Roles = "admin,user")]
    [HttpGet("page-number={page}/page-size={pageSize}")]
    public async Task<IActionResult> Get(int page, int pageSize, string? categories)
    {
        var categoruList = categories is not null ? categories.Split("&").ToList() : null;
        var result = await dispatcher.DispatchQueryAsync<GetProductsQuery,Result<List<ProductDTO>>>(new GetProductsQuery(page, pageSize, categoruList));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = "admin,user")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(Guid id)
    {
        var result = await dispatcher.DispatchQueryAsync<GetProductQuery,Result<ProductDTO>>(new GetProductQuery(id));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await dispatcher.DispatchCommandAsync<DeleteProductCommand,Result<string>>(new DeleteProductCommand(id));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        var result = await dispatcher.DispatchCommandAsync<UpdateProductCommand,Result<ProductDTO>>(command);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Create(AddProductCommand command)
    {
        var result = await dispatcher.DispatchCommandAsync<AddProductCommand,Result<ProductDTO>>(command);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}

