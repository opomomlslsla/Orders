using Domain.Interfaces.Commands;

namespace Application.Commands.Orders;

public record AddOrderCommand(Guid CustomerId) : ICommand;