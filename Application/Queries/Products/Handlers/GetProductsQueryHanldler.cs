using Application.DTO;
using Domain.Entities;
using Domain.Interfaces.Queries;
using FluentValidation;
using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.Handlers;

public class GetProductsQueryHanldler(UnitOfWork unitOfWork, IValidator<GetProductsQuery> validator) : IQueryHandler<GetProductsQuery, Result<List<ProductDTO>>>
{
    public async Task<Result<List<ProductDTO>>> HandleAsync(GetProductsQuery query)
    {
        var validation = await validator.ValidateAsync(query);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
            return new Result<List<ProductDTO>>(null, false, string.Join("\n",errors), 400);
        }
        List<Product>? result;
        var queryable = unitOfWork.ProductRepository.GetQuery()
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);
        if (query.Categories is not null && query.Categories.Any())
        {
            result = await queryable.ToListAsync();
            return new Result<List<ProductDTO>>(result.Adapt<List<ProductDTO>>(), true, "success");
        }
        result = await queryable.Where(p => p.Category != null && query.Categories.Contains(p.Category)).ToListAsync();
        return new Result<List<ProductDTO>>(result.Adapt<List<ProductDTO>>(), true, "success");
    }
}