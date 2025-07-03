using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace BookBarter.Application.Users.Commands;

public class DeleteUserCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly UserManager<User> _userManager;
    public DeleteUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null) { throw new EntityNotFoundException(typeof(User).Name, request.Id); }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to delete user: {errors}");
        }
    }
}
