using FluentValidation;

namespace BookBarter.Application.Genres.Queries;

public class GetPagedGenresQueryValidator : AbstractValidator<GetPagedGenresQuery>
{
    public GetPagedGenresQueryValidator()
    {
        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "Name" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: Name.");
    }
}