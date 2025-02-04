using Application.DTO;
using Domain.Interfaces.Commands;
using Infrastructure;
using Mapster;

namespace Application.Commands.Products.Handlers;

public class UpdateProductCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand, Result<ProductDTO>>
{
    public async Task<Result<ProductDTO>> HandleAsync(UpdateProductCommand command)
    {
        var product = await unitOfWork.ProductRepository.FirstAsync(x => x.Id == command.Id); 
        if (product == null)
            return new Result<ProductDTO>(null, false,$"Product with id {command.Id} does not exist");
        product.Name = command.Name;
        product.Price = command.Price;
        product.Category = command.Category;
        unitOfWork.ProductRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return new Result<ProductDTO>(product.Adapt<ProductDTO>(),false,$"Product with id {command.Id} has been updated");
    }
}