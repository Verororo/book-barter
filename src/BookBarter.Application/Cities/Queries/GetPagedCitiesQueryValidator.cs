using FluentValidation;

namespace BookBarter.Application.Cities.Queries;

public class GetPagedCitiesQueryValidator : AbstractValidator<GetPagedCitiesQuery>
{
    public GetPagedCitiesQueryValidator()
    {
        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "NameWithCountry" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: NameWithCountry.");
    }
}

