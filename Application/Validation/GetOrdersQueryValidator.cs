using Application.Queries.Orders;
using Domain.Enums;
using FluentValidation;

namespace Application.Validation;

public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.StatusFilter).IsEnumName(typeof(OrderStatus), false);
    }
}