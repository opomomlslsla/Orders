using Domain.Interfaces.Commands;

namespace Application.Commands.Users;

public record DeleteUserCommand(Guid UserId) : ICommand;