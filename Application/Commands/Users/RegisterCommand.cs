using Domain.Interfaces.Commands;

namespace Application.Commands.Users;

public record RegisterCommand(
    string Name,
    string Password,
    string Login,
    string Role,
    int Discount = 0,
    string? Address = null) : ICommand;
