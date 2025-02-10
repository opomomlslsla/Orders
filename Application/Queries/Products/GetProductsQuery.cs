using Domain.Interfaces.Queries;

namespace Application.Queries.Products;

public record GetProductsQuery(int Page, int PageSize, List<string>? Categories) : IQuery;