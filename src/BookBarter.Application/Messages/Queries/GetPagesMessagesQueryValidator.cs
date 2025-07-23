using FluentValidation;

namespace BookBarter.Application.Messages.Queries;

public class GetPagedMessagesQueryValidator : AbstractValidator<GetPagedMessagesQuery>
{
    public GetPagedMessagesQueryValidator()
    {
        RuleFor(x => x.CollocutorId)
            .GreaterThan(0);

        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "SentTime" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: SentTime.");
    }
}