using MediatR;
using BookBarter.Application.Auth.Interfaces;
using BookBarter.Application.Auth.Responses;
using Azure.Core;

namespace BookBarter.Application.Auth.Commands;

public class LoginCommand : IRequest<LoginDto>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginDto = await _authenticationService.PasswordLoginAsync(request.Email, 
            request.Password);

        return loginDto;
    }
}