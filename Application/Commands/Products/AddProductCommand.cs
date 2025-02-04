using Domain.Interfaces.Commands;

namespace Application.Commands.Products;

public record AddProductCommand(string Name, decimal Price, string Category): ICommand;