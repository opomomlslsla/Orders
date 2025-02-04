using Domain.Interfaces.Commands;

namespace Application.Commands.Orders;

public record DeleteOrderCommand(Guid OrderId) : ICommand;