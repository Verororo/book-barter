
using FluentValidation;

namespace BookBarter.Application.Authors.Queries;

public class GetPagedAuthorsForModerationQueryValidator : AbstractValidator<GetPagedAuthorsForModerationQuery>
{
    public GetPagedAuthorsForModerationQueryValidator()
    {
        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x) 
                || new[] { "AddedDate", "LastName" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: LastOnlineDate, RegistrationDate, UserName.");
    }
}
