using Domain.Interfaces.Queries;

namespace Application.Queries.Users;

public record GetUsersQuery(string? NameFilter) : IQuery;