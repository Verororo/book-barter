
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookBarter.Application.Auth.Interfaces;
using BookBarter.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookBarter.Infrastructure.Auth;

public class TokenService : ITokenService
{
    private readonly AuthOptions _authenticationOptions;

    public TokenService(IOptions<AuthOptions> authenticationOptions)
    {
        _authenticationOptions = authenticationOptions.Value;
    }

    public string GenerateAccessToken(List<Claim> claims)
    {
        var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), 
            SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
             issuer: _authenticationOptions.Issuer,
             audience: _authenticationOptions.Audience,
             claims: claims,
             expires: DateTime.Now.AddDays(1),
             signingCredentials: signinCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);
        return encodedToken;
    }
}
