using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Authors.Queries;

public class GetPagedAuthorsQueryValidator : AbstractValidator<GetPagedAuthorsQuery>
{
    public GetPagedAuthorsQueryValidator()
    {
        RuleFor(x => x.IdsToSkip)
            .ForEach(x => x.GreaterThan(0));

        RuleFor(x => x.OrderByProperty)
            .Must(x => string.IsNullOrEmpty(x)
                || new[] { "LastName" }.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("OrderByProperty must be one of: LastName.");
    }
}
