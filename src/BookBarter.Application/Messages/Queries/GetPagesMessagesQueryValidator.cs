using BookBarter.Application.Genres.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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