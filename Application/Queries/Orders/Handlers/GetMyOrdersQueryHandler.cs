using Application.DTO;
using Domain.Interfaces.Queries;
using Infrastructure;
using Mapster;

namespace Application.Queries.Orders.Handlers;

public class GetMyOrdersQueryHandler(UnitOfWork unitOfWork) : IQueryHandler<GetMyOrdersQuery,Result<List<OrderDTO>>>
{
    public async Task<Result<List<OrderDTO>>> HandleAsync(GetMyOrdersQuery query)
    {
        var result = new List<OrderDTO>();
        var orders = await unitOfWork.OrderRepository.GetAsync(x => x.CustomerId == query.CustomerId);
        return new Result<List<OrderDTO>>(orders.Adapt<List<OrderDTO>>(), true, "orders");
    }
}