using Application.DTO;
using Domain.Interfaces.Queries;
using FluentValidation;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Orders.Handlers;

public class GetOrdersQueryHandler(UnitOfWork unitOfWork, IValidator<GetOrdersQuery> validator) : IQueryHandler<GetOrdersQuery, Result<List<OrderDTO>>>
{
    public async Task<Result<List<OrderDTO>>> HandleAsync(GetOrdersQuery query)
    {
        var validation = await validator.ValidateAsync(query);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
            return new Result<List<OrderDTO>>(null, false, string.Join("\n",errors), 400);
        }
        var result = await unitOfWork.OrderRepository.GetQuery()
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Where(o => o.Status.ToString() == query.StatusFilter).ToListAsync();
        return new Result<List<OrderDTO>>(result.Adapt<List<OrderDTO>>(), true, "success");
    }
}