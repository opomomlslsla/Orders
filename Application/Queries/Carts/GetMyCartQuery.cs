using Domain.Interfaces.Queries;

namespace Application.Queries.Carts;

public record GetMyCartQuery(Guid CustomerId) : IQuery;