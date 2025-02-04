using Domain.Interfaces.Queries;

namespace Application.Queries.Orders;

public record GetMyOrdersQuery(Guid CustomerId) : IQuery;