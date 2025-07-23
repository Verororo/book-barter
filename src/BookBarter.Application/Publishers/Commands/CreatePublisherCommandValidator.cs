using BookBarter.Application.Messages.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Publishers.Commands;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(p => p.Name)
            .MaximumLength(50);
    }
}
