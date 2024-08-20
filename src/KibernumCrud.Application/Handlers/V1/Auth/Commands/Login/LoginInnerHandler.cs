using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KibernumCrud.Application.Models.V1.Security;
using KibernumCrud.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;

namespace KibernumCrud.Application.Handlers.V1.Auth.Commands.Login;

internal static class LoginInnerHandler
{
    public static string BuildToken(User user, JwtSettings jwtSettings)
    {
        string result;
        
        var claims = new List<Claim>()
        {
            new (ClaimType.ClaimEmailType, user.Email),
            new (ClaimType.ClaimIdType, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.Now.AddHours(8);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: signCredential
        );

        result = new JwtSecurityTokenHandler().WriteToken(token);
        return result;
    }
}