using Domain.Interfaces.Commands;

namespace Application.Commands.Users;

public record UpdateUserCommand(Guid UserId, string? Login, string? Password, int? Discount, string? Address) : ICommand;