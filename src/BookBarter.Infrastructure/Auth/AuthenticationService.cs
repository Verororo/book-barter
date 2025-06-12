using BookBarter.Application.Common.Interfaces.Auth;
using BookBarter.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookBarter.Infrastructure.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> PasswordSignInAsync(string userName, string password)
    {
        var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);

        return checkingPasswordResult.Succeeded;
    }
}