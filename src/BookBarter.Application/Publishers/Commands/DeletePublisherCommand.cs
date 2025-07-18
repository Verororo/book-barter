using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Publishers.Commands;

public class DeletePublisherCommand : IRequest
{
    public int Id { get; set; }
}

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
{
    private readonly IGenericRepository _repository;
    public DeletePublisherCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _repository.GetByIdAsync<Publisher>(request.Id, cancellationToken, p => p.Books);
        if (publisher == null) throw new EntityNotFoundException(typeof(Publisher).Name, request.Id);  // FIX: use validator

        if (publisher.Books != null && publisher.Books.Any())
        {
            var bookIds = string.Join(", ", publisher.Books.Select(b => b.Id));

            var message = $"Attempted to delete Publisher {publisher.Id} referred by existing Books: {bookIds}.";
            throw new BusinessLogicException(message);
        }

        _repository.Delete(publisher);
        await _repository.SaveAsync(cancellationToken);
    }
}
