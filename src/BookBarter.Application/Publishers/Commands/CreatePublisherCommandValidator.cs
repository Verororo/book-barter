using FluentValidation;

namespace BookBarter.Application.Publishers.Commands;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(p => p.Name)
            .MaximumLength(50);
    }
}
