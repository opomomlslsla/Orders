using Application.DTO;
using Domain.Interfaces.Queries;
using Infrastructure;
using Mapster;

namespace Application.Queries.Products.Handlers;

public class GetProductQueryHandler(UnitOfWork unitOfWork) : IQueryHandler<GetProductQuery, Result<ProductDTO>>
{
    public async Task<Result<ProductDTO>> HandleAsync(GetProductQuery query)
    {
        var result = await unitOfWork.ProductRepository.FirstAsync(p => p.Id == query.Id);
        if(result == null)
            return new Result<ProductDTO>(null, false, "Product not found", 404);
        return new Result<ProductDTO>(result.Adapt<ProductDTO>(), true, "Product found");
    }
}