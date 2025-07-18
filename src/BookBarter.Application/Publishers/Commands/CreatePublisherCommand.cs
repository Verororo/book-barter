using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Publishers.Commands;

public class CreatePublisherCommand : IRequest<int?>
{
    public string Name { get; set; } = default!;
}

public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, int?>
{
    private readonly IGenericRepository _repository;
    public CreatePublisherCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<int?> Handle(CreatePublisherCommand command, CancellationToken cancellationToken)
    {
        var publisher = new Publisher
        {
            Name = command.Name,
            AddedDate = DateTime.UtcNow
        };

        _repository.Add(publisher);
        await _repository.SaveAsync(cancellationToken);

        return publisher.Id;
    }
}