using Application.DTO;
using Domain.Interfaces.Commands;
using Infrastructure;
using Mapster;

namespace Application.Commands.Users.Handlers;

public class UpdateUserCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> HandleAsync(UpdateUserCommand command)
    {
        var user = await unitOfWork.UserRepository.FirstAsync(x => x.Id == command.UserId, u => u.Customer);
        if (user == null)
            return new Result<UserDTO>(null, false, "не удалось найти пользователя");
        user.Password = command.Password ?? user.Password;
        user.Login = command.Login ?? user.Login;
        var customer = user.Customer;
        if (customer != null)
        {
            customer.Discount = command.Discount ?? customer.Discount;
            customer.Address = command.Address ?? customer.Address;
            unitOfWork.CustomerRepository.Update(customer);
        }
        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
        return new Result<UserDTO>(user.Adapt<UserDTO>(), true, "пользователь успешно обновлен");
    }
}