using Application.DTO;
using Domain.Entities;
using Domain.Interfaces.Queries;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.Handlers;

public class GetProductsQueryHanldler(UnitOfWork unitOfWork) : IQueryHandler<GetProductsQuery, Result<List<ProductDTO>>>
{
    public async Task<Result<List<ProductDTO>>> HandleAsync(GetProductsQuery query)
    {
        List<Product>? result;
        var queryable = unitOfWork.ProductRepository.GetQuery()
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);
        if (!query.Categories.Any())
        {
            result = await queryable.ToListAsync();
            return new Result<List<ProductDTO>>(result.Adapt<List<ProductDTO>>(), true, "success");
        }
        queryable.Where(p => p.Category != null && query.Categories.Contains(p.Category));
        result = await queryable.ToListAsync();
        return new Result<List<ProductDTO>>(result.Adapt<List<ProductDTO>>(), true, "success");

    }
}