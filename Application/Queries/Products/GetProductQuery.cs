using Domain.Interfaces.Queries;

namespace Application.Queries.Products;

public record GetProductQuery(Guid Id) : IQuery;