using Domain.Enums;
using Domain.Interfaces.Commands;
using Infrastructure;

namespace Application.Commands.Users.Handlers;

public class DeleteUserCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(DeleteUserCommand command)
    {
        var user = await unitOfWork.UserRepository.FirstAsync(x => x.Id == command.UserId);
        if (user == null)
            return new Result<string>("fail", false, $"no users have been found by given Id : {command.UserId}", 404);
        var customer = user.Role != Role.Manager ? await unitOfWork.CustomerRepository.FirstAsync(x=> x.Id == user.CustomerId) : null;
        if(customer != null)
            unitOfWork.CustomerRepository.Delete(customer);
        unitOfWork.UserRepository.Delete(user);
        return new Result<string>("success", true, $"user deleted : {user.Id}");
    }
}