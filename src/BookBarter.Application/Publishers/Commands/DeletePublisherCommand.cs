using BookBarter.Application.Common.Interfaces;
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
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public DeletePublisherCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }
    public async Task Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _repository.GetByIdAsync<Publisher>(request.Id, cancellationToken, p => p.Books);
        _entityExistenceValidator.ValidateAsync(publisher, request.Id);

        if (publisher.Books != null && publisher.Books.Count != 0)
        {
            var bookIds = string.Join(", ", publisher.Books.Select(b => b.Id));

            var message = $"Attempted to delete Publisher {publisher.Id} referred by existing Books: {bookIds}.";
            throw new BusinessLogicException(message);
        }

        _repository.Delete(publisher);
        await _repository.SaveAsync(cancellationToken);
    }
}
