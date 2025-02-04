using Domain.Interfaces.Commands;

namespace Application.Commands.Carts;

public record AddToCartCommand(Guid ConsumerId, Guid ProductId, int Quantity) : ICommand;
