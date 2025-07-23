using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Messages.Commands;

public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{
    public UpdateMessageCommandValidator()
    {
        RuleFor(m => m.Id)
            .GreaterThan(0);

        RuleFor(m => m.Body)
            .MaximumLength(500);
    }
}