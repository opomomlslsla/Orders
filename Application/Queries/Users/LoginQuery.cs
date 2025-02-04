using Domain.Interfaces.Queries;

namespace Application.Queries.Users;

public record LoginQuery(string Login, string Password) : IQuery;