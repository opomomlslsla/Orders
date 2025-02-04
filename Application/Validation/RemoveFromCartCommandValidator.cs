using Application.Commands.Carts;
using FluentValidation;

namespace Application.Validation;

public class RemoveFromCartCommandValidator : AbstractValidator<RemoveFromCartCommand>
{
    public RemoveFromCartCommandValidator()
    {
        RuleFor(x => x.Quantity).InclusiveBetween(1, 1000);
    }
}