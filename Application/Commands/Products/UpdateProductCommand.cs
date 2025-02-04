using Domain.Interfaces.Commands;

namespace Application.Commands.Products;

public record UpdateProductCommand(Guid Id, decimal Price, string Category, string Name) : ICommand;
