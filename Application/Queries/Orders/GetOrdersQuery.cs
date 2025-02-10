using Domain.Interfaces.Queries;

namespace Application.Queries.Orders;

public record GetOrdersQuery(int Page, int PageSize, string? StatusFilter) : IQuery;    
