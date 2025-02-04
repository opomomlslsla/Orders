using Application.Commands.Users;
using Domain.Enums;
using FluentValidation;

namespace Application.Validation;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Login).NotNull().NotEmpty().MinimumLength(5);
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(7);
        RuleFor(x => x.Role).IsEnumName(typeof(Role), false);
    }
}