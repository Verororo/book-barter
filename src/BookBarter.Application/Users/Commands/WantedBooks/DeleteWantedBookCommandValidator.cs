using BookBarter.Application.Users.Commands.WantedBooks;
using FluentValidation;

public class DeleteWantedBookCommandValidator : AbstractValidator<DeleteWantedBookCommand>
{
    public DeleteWantedBookCommandValidator()
    {
        RuleFor(wb => wb.BookId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
