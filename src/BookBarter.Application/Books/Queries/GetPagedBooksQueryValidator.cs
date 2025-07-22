
using BookBarter.Application.Authors.Queries;
using FluentValidation;

namespace BookBarter.Application.Books.Queries;

public class GetPagedBooksQueryValidator : AbstractValidator<GetPagedBooksQuery>
{
    public GetPagedBooksQueryValidator()
    {
        RuleFor(x => x.GenreId)
            .GreaterThan(0);

        RuleFor(x => x.PublisherId)
            .GreaterThan(0);

        RuleFor(x => x.AuthorId)
            .GreaterThan(0);

        RuleFor(x => x.IdsToSkip)
            .ForEach(x => x.GreaterThan(0));

        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "Title", "PublicationDate" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: Title, PublicationDate.");
    }
}
