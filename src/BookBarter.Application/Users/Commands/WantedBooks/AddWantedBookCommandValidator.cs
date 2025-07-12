
using FluentValidation;

namespace BookBarter.Application.Users.Commands.WantedBooks;

public class AddWantedBookCommandValidator : AbstractValidator<AddWantedBookCommand>
{
    public AddWantedBookCommandValidator()
    {
        RuleFor(wb => wb.BookId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
