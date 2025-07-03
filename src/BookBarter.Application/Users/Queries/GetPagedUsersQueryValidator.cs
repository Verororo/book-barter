
using FluentValidation;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Application.Users.Queries;

public class GetPagedUsersQueryValidator : AbstractValidator<GetPagedUsersQuery>
{
    public GetPagedUsersQueryValidator()
    {
        RuleFor(x => x.OrderByProperty)
            .NotEqual("wantedBooks", StringComparer.OrdinalIgnoreCase)
            .NotEqual("ownedBooks", StringComparer.OrdinalIgnoreCase);
    }
}
