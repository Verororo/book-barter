using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Authors.Commands;

public class CreateAuthorCommand : IRequest<int>
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
{
    private readonly IGenericRepository _repository;
    public CreateAuthorCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            FirstName = command.FirstName,
            MiddleName = command.MiddleName,
            LastName = command.LastName,
            AddedDate = DateTime.UtcNow
        };

        _repository.Add(author);
        await _repository.SaveAsync(cancellationToken);

        return author.Id;
    }
}