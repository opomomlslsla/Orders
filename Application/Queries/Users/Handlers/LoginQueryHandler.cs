using Domain.Interfaces;
using Domain.Interfaces.Queries;
using FluentValidation;
using Infrastructure;

namespace Application.Queries.Users.Handlers;

public class LoginQueryHandler(UnitOfWork unitOfWork, IJWTProvider jwtProvider, IValidator<LoginQuery> validator) : IQueryHandler<LoginQuery, Result<string>>
{
    public async Task<Result<string>> HandleAsync(LoginQuery query)
    {
        var validation = await validator.ValidateAsync(query);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
            return new Result<string>("Validation failed", false, string.Join("\n",errors));
        }
        
        var user = await unitOfWork.UserRepository.FirstAsync(u => query.Login == u.Login);
        if (user == null)
            return new Result<string>("fail", false, "cant't find user with the given login");
        if(user.Password != query.Password)
            return new Result<string>("fail", false, $"password or login is incorrect.");
        var token = jwtProvider.GenerateToken(user);
        return new Result<string>(token, true, "success");
    }
}