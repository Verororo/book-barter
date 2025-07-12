using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
