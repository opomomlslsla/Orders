using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Tools;

public class JWTProvider(IOptions<JwtOptions> options) : IJWTProvider
{
    JwtOptions _options = options.Value;
    public string GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>() 
        { 
            new Claim(ClaimTypes.Name, user.Login), 
            new Claim(ClaimTypes.Role, user.Role == Role.Manager ? "admin" : "user")
        };

        var Credentials = new SigningCredentials(key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: Credentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresIn));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}