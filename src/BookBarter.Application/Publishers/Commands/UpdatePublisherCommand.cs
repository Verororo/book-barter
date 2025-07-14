
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Publishers.Commands;

public class UpdatePublisherCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand>
{
    private readonly IGenericRepository _repository;
    public UpdatePublisherCommandHandler(
        IGenericRepository repository
        )
    {
        _repository = repository;
    }
    public async Task Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _repository.GetByIdAsync<Publisher>(request.Id, cancellationToken);
        if (publisher == null) throw new EntityNotFoundException(typeof(Publisher).Name, request.Id);

        publisher.Name = request.Name;

        await _repository.SaveAsync(cancellationToken);
    }
}