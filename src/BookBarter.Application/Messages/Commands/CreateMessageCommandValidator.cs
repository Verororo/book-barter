using BookBarter.Application.Authors.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Messages.Commands;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(m => m.ReceiverId)
            .GreaterThan(0);

        RuleFor(m => m.Body)
            .MaximumLength(500);
    }
}

