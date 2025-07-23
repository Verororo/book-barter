using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Publishers.Commands;

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0);

        RuleFor(p => p.Name)
            .MaximumLength(50);
    }
}

