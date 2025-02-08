using Application.DTO;
using Domain.Interfaces.Queries;
using Infrastructure;
using Mapster;

namespace Application.Queries.Carts.Handlers;

public class GerMyCartQueryHandler(UnitOfWork unitOfWork) : IQueryHandler<GetMyCartQuery, Result<CartDTO>> 
{
    public async Task<Result<CartDTO>> HandleAsync(GetMyCartQuery query)
    {
        var cart = await unitOfWork.CartRepository.FirstAsync(x => x.Id == query.CustomerId);
        if (cart == null)
            return new Result<CartDTO>(null, false, $"Can't find cart for user with id: {query.CustomerId}", 404);
        return new Result<CartDTO>(cart.Adapt<CartDTO>(), true, "cart");
    }
}