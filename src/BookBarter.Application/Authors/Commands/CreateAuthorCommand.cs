using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Authors.Commands;

public class CreateAuthorCommand : IRequest
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
}

public class CreateAuthorCommandHandler : IRequest<CreateAuthorCommand>
{
    private readonly IGenericRepository _repository;
    public CreateAuthorCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            FirstName = command.FirstName,
            MiddleName = command.MiddleName,
            LastName = command.LastName,
        };

        _repository.Add<Author>(author);
        await _repository.SaveAsync(cancellationToken);
    }
}