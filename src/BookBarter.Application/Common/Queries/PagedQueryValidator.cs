
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Common.Models;
using FluentValidation;

namespace BookBarter.Application.Common.Queries;

public class PagedQueryValidator<T> : AbstractValidator<T> where T : PagedQuery
{
    public PagedQueryValidator()
    {
        RuleFor(q => q.PageNumber)
            .GreaterThan(0);

        RuleFor(q => q.PageSize)
            .LessThan(50);

        var orderDirections = new List<string> { "asc", "desc" };
        RuleFor(q => q.OrderDirection)
            .Must(d => orderDirections.Contains(d))
            .WithMessage("The order direction must be either 'asc' (ascending) or 'desc' (descending).");

        // property validation?
    }
}
