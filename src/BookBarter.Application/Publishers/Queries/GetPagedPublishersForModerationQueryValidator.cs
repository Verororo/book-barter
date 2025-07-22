using BookBarter.Application.Genres.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Publishers.Queries;

public class GetPagedPublishersForModerationQueryValidator : AbstractValidator<GetPagedPublishersForModerationQuery>
{
    public GetPagedPublishersForModerationQueryValidator()
    {
        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "Name", "AddedDate" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: Name, AddedDate.");
    }
}