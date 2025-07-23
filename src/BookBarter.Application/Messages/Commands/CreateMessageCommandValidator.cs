using FluentValidation;

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

