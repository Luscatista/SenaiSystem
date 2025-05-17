using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SenaiSystem.Services;

public class TokenService
{
    public string GenerateToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kw%!wZ6rzv9V9yCg9WvZbbJgvs7US8Go%h66E22d"));

        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: "senaiSystem",
            audience: "senaiSystem",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
