using Domain.Interfaces.Commands;

namespace Application.Commands.Products;

public record DeleteProductCommand(Guid ProductId) : ICommand;