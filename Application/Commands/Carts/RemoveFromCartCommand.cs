using Domain.Interfaces.Commands;

namespace Application.Commands.Carts;

public record RemoveFromCartCommand(Guid CustomerId, Guid ProductId, int Quantity) : ICommand;
