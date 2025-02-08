using Application.DTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Commands;
using FluentValidation;
using Infrastructure;
using Mapster;

namespace Application.Commands.Users.Handlers;

internal class RegisterCommandHandler(UnitOfWork unitOfWork, IJWTProvider jwtProvider, IValidator<RegisterCommand> validator) : ICommandHandler<RegisterCommand,Result<UserDTO>>
{
    public async Task<Result<UserDTO>> HandleAsync(RegisterCommand command)
    {
        var validation = await validator.ValidateAsync(command);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
            return new Result<UserDTO>(null, false, "Validation Failed " + string.Join("\n",errors),400);
        }
        var user = new User
        {
            Login = command.Login,
            Password = command.Password,
            Role = Enum.Parse<Role>(command.Role, true)
        };

        if (command.Role?.ToLower() == "Manager")
        {
            await unitOfWork.UserRepository.AddAsync(user);
            return new Result<UserDTO>(user.Adapt<UserDTO>(), false, "admin user has been added successfully");
        }
        var customer = new Customer
        {
            Code = DateTime.Now.ToString("ssmm-yyyy"),
            Name = command.Name,
            Address = command.Address,
            Discount = command.Discount
        };
        user.CustomerId = await unitOfWork.CustomerRepository.AddAsync(customer);
        await unitOfWork.UserRepository.AddAsync(user);
        var cart = new Cart()
        {
            CustomerId = customer.Id,
        };
        await unitOfWork.CartRepository.AddAsync(cart);
        await unitOfWork.SaveChangesAsync();
        var token = jwtProvider.GenerateToken(user);
        return new Result<UserDTO>(user.Adapt<UserDTO>(), false, "user has been added successfully");
    }

}