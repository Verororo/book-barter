using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Authors.Commands;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(a => a.Id)
            .GreaterThan(0);

        RuleFor(a => a.FirstName)
            .MaximumLength(30);

        RuleFor(a => a.MiddleName)
            .MaximumLength(30);

        RuleFor(a => a.LastName)
            .NotEmpty()
            .MaximumLength(30);
    }
}
