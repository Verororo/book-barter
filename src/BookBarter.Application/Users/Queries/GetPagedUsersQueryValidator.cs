
using FluentValidation;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Application.Users.Queries;

public class GetPagedUsersQueryValidator : AbstractValidator<GetPagedUsersQuery>
{
    public GetPagedUsersQueryValidator()
    {
        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x) 
                || new[] { "LastOnlineDate", "RegistrationDate", "UserName" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: LastOnlineDate, RegistrationDate, UserName");
    }
}
