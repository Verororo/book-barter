using FluentValidation;

namespace BookBarter.Application.Publishers.Queries;

public class GetPagedPublishersQueryValidator : AbstractValidator<GetPagedPublishersQuery>
{
    public GetPagedPublishersQueryValidator()
    {
        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "Name" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: Name.");
    }
}