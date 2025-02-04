using Domain.Interfaces.Commands;

namespace Application.Commands.Orders;

public record ConfirmOrderCommand(Guid OrderId) : ICommand;