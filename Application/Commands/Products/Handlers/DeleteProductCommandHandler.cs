using Domain.Interfaces.Commands;
using Infrastructure;

namespace Application.Commands.Products.Handlers;

public class DeleteProductCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<DeleteProductCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(DeleteProductCommand command)
    {
        var product = await unitOfWork.ProductRepository.FirstAsync(x => x.Id == command.ProductId);
        if(product != null)
            return new Result<string>("success", true, "product deleted");
        await unitOfWork.SaveChangesAsync();
        return new Result<string>("fail", false, "Product not found");
    }
}