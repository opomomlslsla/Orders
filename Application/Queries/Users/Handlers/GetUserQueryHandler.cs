using Application.DTO;
using Domain.Interfaces.Queries;
using Infrastructure;
using Mapster;

namespace Application.Queries.Users.Handlers;

public class GetUserQueryHandler(UnitOfWork unitOfWork) : IQueryHandler<GetUserQuery, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> HandleAsync(GetUserQuery query)
    {
        var user = await unitOfWork.UserRepository.FirstAsync(x => x.Id == query.Id, u => u.Customer);
        if (user == null)
        {
            return new Result<UserDTO>(null, false, $"User with id: {query.Id} not found");
        }
        return new Result<UserDTO>(user.Adapt<UserDTO>(), true, $"User with id: {query.Id} found");
    }
}