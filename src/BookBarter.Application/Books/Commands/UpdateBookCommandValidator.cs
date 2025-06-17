using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BookBarter.Application.Books.Commands;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(b => b.Isbn)
            .NotEmpty()
            .Length(13);

        RuleFor(b => b.Title)
            .NotEmpty()
            .Length(1, 100);

        RuleFor(b => b.PublicationDate)
            .NotEmpty();

        RuleFor(b => b.AuthorsIds)
            .NotEmpty()
            .ForEach(i => i.GreaterThan(0));

        RuleFor(b => b.PublisherId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(b => b.GenreId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
