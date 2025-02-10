using Application.Queries.Products;
using FluentValidation;

namespace Application.Validation;

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
    }
}