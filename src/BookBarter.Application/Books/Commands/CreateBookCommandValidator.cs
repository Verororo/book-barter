
using FluentValidation;

namespace BookBarter.Application.Books.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(b => b.Isbn)
            .NotEmpty() // why not use 'required' in the command?
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
