using Application.DTO;
using Domain.Entities;
using Domain.Interfaces.Queries;
using Infrastructure;
using Mapster;

namespace Application.Queries.Users.Handlers;

public class GetUsersQueryHandler(UnitOfWork unitOfWork) : IQueryHandler<GetUsersQuery, Result<List<UserDTO>>>
{
    public async Task<Result<List<UserDTO>>> HandleAsync(GetUsersQuery query)
    {
        ICollection<User> users;
        if(string.IsNullOrEmpty(query.NameFilter))
            users = await unitOfWork.UserRepository.GetAsync(includes: u => u.Customer);
        users = await unitOfWork.UserRepository.GetAsync(
            u => u.Customer != null && u.Customer.Name.Contains(query.NameFilter ?? String.Empty),
            u => u.Customer);
        return new Result<List<UserDTO>>(users.Adapt<List<UserDTO>>(),true, "Users");
    }
}