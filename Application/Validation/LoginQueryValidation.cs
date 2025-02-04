using Application.Queries.Users;
using FluentValidation;

namespace Application.Validation;

public class LoginQueryValidation : AbstractValidator<LoginQuery>
{
    public LoginQueryValidation()
    {
        RuleFor(x => x.Login).NotNull().NotEmpty().MinimumLength(5);
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(7);
    }
}