using Domain.Interfaces.Queries;

namespace Application.Queries.Users;

public record GetUserQuery(Guid Id) : IQuery;