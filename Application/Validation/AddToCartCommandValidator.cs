using Application.Commands.Carts;
using Domain.Enums;
using FluentValidation;


namespace Application.Validation;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(x => x.Quantity).InclusiveBetween(1, 1000);
    }
}