
using System.Data;
using System.Security.Claims;
using BookBarter.Application.Auth.Interfaces;
using BookBarter.Application.Auth.Responses;
using BookBarter.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookBarter.Infrastructure.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager,
        ITokenService tokenService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<RegisterDto> RegisterUserAsync(string userName, string email, string city, 
        string password)
    {
        var user = new User
        {
            UserName = userName,
            Email = email,
            City = city,
            RegistrationDate = DateTime.UtcNow,
        };

        var result = await _userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
        {
            List<string> errors = result.Errors.Select(e => e.Description).ToList();

            return new RegisterDto { Succeeded = false, Messages = errors };
        }

        await _userManager.AddToRoleAsync(user, "User");

        return new RegisterDto { Succeeded = true, Messages = [] };
    }

    public async Task<LoginDto> PasswordLoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) 
            return new LoginDto { Succeeded = false }; 

        var checkingPasswordResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!checkingPasswordResult.Succeeded)
            return new LoginDto { Succeeded = false };

        var claims = new List<Claim>();
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            var roleClaim = new Claim(ClaimTypes.Role, role);
            claims.Add(roleClaim);
        }

        var accessToken = _tokenService.GenerateAccessToken(claims);
        
        return new LoginDto { AccessToken = accessToken, Succeeded = true };
    }
}