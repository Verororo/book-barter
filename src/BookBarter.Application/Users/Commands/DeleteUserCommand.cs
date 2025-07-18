using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using BookBarter.Application.Common.Interfaces;

namespace BookBarter.Application.Users.Commands;

public class DeleteUserCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public DeleteUserCommandHandler(UserManager<User> userManager, IEntityExistenceValidator entityExistenceValidator)
    {
        _userManager = userManager;
        _entityExistenceValidator = entityExistenceValidator;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        _entityExistenceValidator.ValidateAsync(user, request.Id);

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new BusinessLogicException($"Failed to delete user: {errors}");
        }
    }
}
