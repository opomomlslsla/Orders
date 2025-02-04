using Application;
using Application.Commands.Products;
using Application.DTO;
using Application.Queries.Products;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("products")]
[ApiController]
public class ProductsController(IDispatcher dispatcher) : ControllerBase
{
    [HttpGet("pages/{page}")]
    public async Task<IActionResult> Get(int page, int pageSize, List<string> categories)
    {
        var result = await dispatcher.DispatchQueryAsync<GetProductsQuery,Result<List<ProductDTO>>>(new GetProductsQuery(page, pageSize, categories));
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(Guid id)
    {
        var result = await dispatcher.DispatchQueryAsync<GetProductQuery,Result<ProductDTO>>(new GetProductQuery(id));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await dispatcher.DispatchCommandAsync<DeleteProductCommand,Result<string>>(new DeleteProductCommand(id));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        var result = await dispatcher.DispatchCommandAsync<UpdateProductCommand,Result<ProductDTO>>(command);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddProductCommand command)
    {
        var result = await dispatcher.DispatchCommandAsync<AddProductCommand,Result<ProductDTO>>(command);
        return Ok(result);
    }
}

