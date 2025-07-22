using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Books.Queries;

public class GetPagedBooksForModerationQueryValidator : AbstractValidator<GetPagedBooksForModerationQuery>
{
    public GetPagedBooksForModerationQueryValidator()
    {
        RuleFor(x => x.GenreId)
            .GreaterThan(0);

        RuleFor(x => x.PublisherId)
            .GreaterThan(0);

        RuleFor(x => x.AuthorId)
            .GreaterThan(0);

        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "Title", "AddedDate", "PublicationDate" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: Title, AddedDate, PublicationDate.");
    }
}