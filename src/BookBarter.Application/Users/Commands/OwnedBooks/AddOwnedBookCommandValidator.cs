
using FluentValidation;

namespace BookBarter.Application.Users.Commands.OwnedBooks;

public class AddOwnedBookCommandValidator : AbstractValidator<AddOwnedBookCommand>
{
    public AddOwnedBookCommandValidator()
    {
        RuleFor(ob => ob.BookId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(ob => ob.BookStateId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
