
using System.Reflection;
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
            .LessThanOrEqualTo(100);

        var orderDirections = new List<string> { "asc", "desc" };
        RuleFor(q => q.OrderDirection)
            .Must(d => orderDirections.Contains(d))
            .WithMessage("The order direction must be either 'asc' (ascending) or 'desc' (descending).");
        /*
        RuleFor(q => q.OrderByProperty)
            .Must((q, prop) => {
                if (string.IsNullOrWhiteSpace(prop))
                    return false;

                var type = q.GetType();
                var found = type.GetProperty(
                    prop,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                return found != null;
            })
            .WithMessage(q => $"OrderByProperty '{q.OrderByProperty}' is not a valid property of {q.GetType().Name}.");
        */
    }
}
