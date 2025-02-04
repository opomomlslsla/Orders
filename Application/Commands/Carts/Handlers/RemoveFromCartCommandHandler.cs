using Domain.Interfaces.Commands;
using FluentValidation;
using Infrastructure;

namespace Application.Commands.Carts.Handlers;

public class RemoveFromCartCommandHandler(UnitOfWork unitOfWork, IValidator<RemoveFromCartCommand> validator) : ICommandHandler<RemoveFromCartCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(RemoveFromCartCommand command)
    {
        var validation = await validator.ValidateAsync(command);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
            return new Result<string>("Validation failed", false, string.Join("\n",errors));
        }
        
        var cart = await unitOfWork.CartRepository.FirstAsync(c => c.CustomerId == command.CustomerId);
        if (cart == null)
            return new Result<string>("Fail", false, "Cannot find cart");
        var cartProduct = cart.Products.FirstOrDefault(cp => cp.ProductId == command.ProductId);
        cartProduct.Quatity -= command.Quantity;
        
        if (cartProduct.Quatity < 0) 
            return new Result<string>("Fail", false, "Cart has less Items than requested to delete");
        
        unitOfWork.CartRepository.Update(cart);
        await unitOfWork.SaveChangesAsync();
        return new Result<string>("Success", true, "Item has been removed from your cart");
    }
}