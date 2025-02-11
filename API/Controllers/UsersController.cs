using Application;
using Application.Commands.Users;
using Application.DTO;
using Application.Queries.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("users")]
[ApiController]
public class UsersController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginQuery loginQuery)
    {
        var result = await dispatcher.DispatchQueryAsync<LoginQuery,Result<LoginDTO>>(loginQuery);
        if (result.Value != null) HttpContext.Response.Cookies.Append("tasty-token", result.Value.Token);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles ="admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand registrationCommand)
    {
        var result = await dispatcher.DispatchCommandAsync<RegisterCommand, Result<UserDTO>>(registrationCommand);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
    [Authorize(Roles ="admin")]
    [HttpGet("filterword={filter}")]
    public async Task<IActionResult> Get(string? filter = null)
    {
        var result = await dispatcher.DispatchQueryAsync<GetUsersQuery, Result<List<UserDTO>>>(new GetUsersQuery(filter));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles ="admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await dispatcher.DispatchQueryAsync<GetUserQuery, Result<UserDTO>>(new GetUserQuery(id));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles ="admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await dispatcher.DispatchCommandAsync<DeleteUserCommand, Result<string>>(new DeleteUserCommand(id));
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles ="admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserCommand updateUserCommand)
    {
        var result = await dispatcher.DispatchCommandAsync<UpdateUserCommand, Result<UserDTO>>(updateUserCommand);
        return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
    }
}