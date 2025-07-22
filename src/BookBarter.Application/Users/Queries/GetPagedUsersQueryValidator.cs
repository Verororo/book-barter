
using FluentValidation;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Application.Users.Queries;

public class GetPagedUsersQueryValidator : AbstractValidator<GetPagedUsersQuery>
{
    public GetPagedUsersQueryValidator()
    {
        RuleFor(x => x.CityId)
            .GreaterThan(0);


        RuleFor(x => x.WantedBooksIds)
            .ForEach(x => x.GreaterThan(0));

        RuleFor(x => x.WantedBookAuthorsIds)
            .ForEach(x => x.GreaterThan(0));

        RuleFor(x => x.WantedBookGenreId)
            .GreaterThan(0);

        RuleFor(x => x.WantedBookPublisherId)
            .GreaterThan(0);


        RuleFor(x => x.OwnedBooksIds)
            .ForEach(x => x.GreaterThan(0));

        RuleFor(x => x.OwnedBookAuthorsIds)
            .ForEach(x => x.GreaterThan(0));

        RuleFor(x => x.OwnedBookGenreId)
            .GreaterThan(0);

        RuleFor(x => x.OwnedBookPublisherId)
            .GreaterThan(0);


        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x) 
                || new[] { "LastOnlineDate", "RegistrationDate", "UserName" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: LastOnlineDate, RegistrationDate, UserName.");
    }
}
