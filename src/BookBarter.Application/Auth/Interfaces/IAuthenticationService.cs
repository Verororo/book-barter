

using BookBarter.Application.Auth.Responses;

namespace BookBarter.Application.Auth.Interfaces;

public interface IAuthenticationService
{
    Task<RegisterDto> RegisterUserAsync(string userName, string email, int cityId, string password);
    Task<LoginDto> PasswordLoginAsync(string userName, string password);
}
