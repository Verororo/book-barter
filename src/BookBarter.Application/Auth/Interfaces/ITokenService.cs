using System.Security.Claims;

namespace BookBarter.Application.Auth.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(List<Claim> claims);
}
