using FluentValidation;

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