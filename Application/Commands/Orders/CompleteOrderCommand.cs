using Domain.Interfaces.Commands;

namespace Application.Commands.Orders;

public record CompleteOrderCommand(Guid OrderId) : ICommand;