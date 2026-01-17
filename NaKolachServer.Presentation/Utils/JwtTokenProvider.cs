using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using NaKolachServer.Domain.Auth;
using NaKolachServer.Presentation.Configurations;

namespace NaKolachServer.Presentation.Utils;

public class JwtTokenProvider(IOptions<JwtAuthConfiguration> options) : IJwtTokenProvider
{
    public string NewAccessToken(string sub, string name)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, sub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, name)
            };

        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string NewRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}