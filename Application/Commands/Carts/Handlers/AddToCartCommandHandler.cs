using System.Text;
using Domain.Interfaces.Commands;
using Domain.ValueObjects;
using FluentValidation;
using Infrastructure;

namespace Application.Commands.Carts.Handlers;

public class AddToCartCommandHandler(UnitOfWork unitOfWork, IValidator<AddToCartCommand> validator) : ICommandHandler<AddToCartCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(AddToCartCommand command)
    {
        var validation = await validator.ValidateAsync(command);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
            return new Result<string>("Validation failed", false, string.Join("\n",errors), 400);
        }
        var customer = await unitOfWork.CustomerRepository.FirstAsync(cos => cos.Id == command.ConsumerId);
        if (customer == null)
            return new Result<string>("Fail",false, "Customer not found", 404);
        var cart = await unitOfWork.CartRepository.FirstAsync(cart => cart.CustomerId == customer.Id);
        var product = await unitOfWork.ProductRepository.FirstAsync(p => p.Id == command.ProductId);
        if (product == null)
            return new Result<string>("Fail", false, "Product not found", 404);
        cart!.Products.Add(new CartProduct(){ProductId = product.Id, Product = product, Quatity = command.Quantity});
        unitOfWork.CartRepository.Update(cart);
        await unitOfWork.SaveChangesAsync();
        return new Result<string>("Success", true, "Product added");
    }
}