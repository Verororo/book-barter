using MediatR;
using BookBarter.Application.Auth.Interfaces;
using BookBarter.Application.Auth.Responses;

namespace BookBarter.Application.Auth.Commands
{
    public class RegisterCommand : IRequest<RegisterDto>
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int CityId { get; set; } = default!;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterDto>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<RegisterDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RegisterUserAsync(request.UserName, request.Email,
                request.CityId, request.Password);

            return result;
        }
    }
}