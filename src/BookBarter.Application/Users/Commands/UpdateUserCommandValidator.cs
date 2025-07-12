
using FluentValidation;

namespace BookBarter.Application.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.About)
            .MaximumLength(300);

        RuleFor(u => u.CityId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
