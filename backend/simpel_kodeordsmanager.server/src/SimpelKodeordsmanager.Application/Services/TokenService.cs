using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Services;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application.Services;

internal class TokenService(IOptions<JwtModel> jwt) : ITokenService
{
    public (string token, int expiresIn) Generate(string email)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Email, email),
        };

        var rsaSecurityKey = GetRsaKey();
        var signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var securityToken = new JwtSecurityToken(
            jwt.Value.Issuer,
            jwt.Value.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(jwt.Value.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        var expiresIn = jwt.Value.DurationInMinutes * 60; // convert minutes to seconds
        
        return (tokenString, expiresIn);
    }

    private RsaSecurityKey GetRsaKey()
    {
        var rsaKey = RSA.Create();
        var pemKey = File.ReadAllText(jwt.Value.PrivateKeyPath);
        rsaKey.ImportFromPem(pemKey);

        var rsaSecurityKey = new RsaSecurityKey(rsaKey);
        return rsaSecurityKey;
    }
}