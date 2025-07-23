

using BookBarter.Application.Authors.Commands;
using FluentValidation;

namespace AuthorBarter.Application.Authors.Commands;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(a => a.FirstName)
            .MaximumLength(30);

        RuleFor(a => a.MiddleName)
            .MaximumLength(30);

        RuleFor(a => a.LastName)
            .NotEmpty()
            .MaximumLength(30);
    }
}
