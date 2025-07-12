
using FluentValidation;

namespace BookBarter.Application.Users.Commands.OwnedBooks;

public class DeleteOwnedBookCommandValidator : AbstractValidator<DeleteOwnedBookCommand>
{
    public DeleteOwnedBookCommandValidator()
    {
        RuleFor(ob => ob.BookId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
