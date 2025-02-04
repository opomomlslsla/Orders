using System.Text;
using Application.DTO;
using Domain.Entities;
using Domain.Interfaces.Commands;
using Infrastructure;
using Mapster;

namespace Application.Commands.Products.Handlers;

public class AddProductCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<AddProductCommand, Result<ProductDTO>>
{
    public async Task<Result<ProductDTO>> HandleAsync(AddProductCommand command)
    {
        var product = new Product()
        {
            Name = command.Name,
            Price = command.Price,
            Category = command.Category,
            Code = GenerateProductCode()
        };
        await unitOfWork.ProductRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return new Result<ProductDTO>(product.Adapt<ProductDTO>(), true, "Product added");
    }
    
    private string GenerateProductCode()
    {
        Random random = Random.Shared;
        var codeBuilder = new StringBuilder();
        codeBuilder.Append(random.Next(10, 100));
        codeBuilder.Append("-");
        for (int i = 0; i < 4; i++)
        {
            codeBuilder.Append(random.Next(0, 10));
        }
        codeBuilder.Append("-");
        for (int i = 0; i < 2; i++)
        {
            char randomLetter = (char)random.Next('A', 'Z' + 1);
            codeBuilder.Append(randomLetter);
        }
        codeBuilder.Append(random.Next(10, 100));
        return codeBuilder.ToString();
    }
}