using Application.DTO;
using Domain.Interfaces;
using Domain.Interfaces.Queries;
using FluentValidation;
using Infrastructure;

namespace Application.Queries.Users.Handlers;

public class LoginQueryHandler(UnitOfWork unitOfWork, IJWTProvider jwtProvider, IValidator<LoginQuery> validator) : IQueryHandler<LoginQuery, Result<LoginDTO>>
{
    public async Task<Result<LoginDTO>> HandleAsync(LoginQuery query)
    {
        var validation = await validator.ValidateAsync(query);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
            return new Result<LoginDTO>(null, false, string.Join("\n",errors), 400);
        }
        
        var user = await unitOfWork.UserRepository.FirstAsync(u => query.Login == u.Login);
        if (user == null)
            return new Result<LoginDTO>(null, false, "cant't find user with the given login", 404);
        if(user.Password != query.Password)
            return new Result<LoginDTO>(null, false, $"password or login is incorrect.", 400);
        var token = jwtProvider.GenerateToken(user);
        return new Result<LoginDTO>(new LoginDTO(token, user.Role.ToString()), true, "success");
    }
}