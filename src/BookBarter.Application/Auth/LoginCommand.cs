using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BookBarter.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using BookBarter.Application.Common.Interfaces.Auth;

namespace BookBarter.Application.Auth.Commands;

public class LoginCommand : IRequest<string>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    // UserManager to infrastructure project
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(
        UserManager<User> userManager,
        IAuthenticationService authenticationService,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _authenticationService = authenticationService;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }

        var passwordSignInResult = await _authenticationService.PasswordSignInAsync(request.UserName, 
            request.Password);
        string? accessToken = null;
        if (passwordSignInResult)
        {
            accessToken = _tokenService.GenerateAccessToken();
        }

        return accessToken;
    }
}