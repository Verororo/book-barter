using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Interfaces;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands.OwnedBooks;

public class AddOwnedBookCommand : IRequest
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int BookStateId { get; set; }
}

public class AddOwnedBookCommandHandler : IRequestHandler<AddOwnedBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IBookRelationshipRepository _bookRelationshipRepository;
    private readonly IEntityExistenceValidator _existenceValidator;
    public AddOwnedBookCommandHandler(IGenericRepository repository,
        IBookRelationshipRepository bookRelationshipRepository,
        IEntityExistenceValidator existenceValidator)
    {
        _repository = repository;
        _bookRelationshipRepository = bookRelationshipRepository;
        _existenceValidator = existenceValidator;
    }
    public async Task Handle(AddOwnedBookCommand request, CancellationToken cancellationToken)
    {
        await _existenceValidator.ValidateAsync<User>(request.UserId, cancellationToken);
        await _existenceValidator.ValidateAsync<Book>(request.BookId, cancellationToken);
        await _existenceValidator.ValidateAsync<BookState>(request.BookStateId, cancellationToken);

        if (await _bookRelationshipRepository.ExistsAsync<OwnedBook>(request.UserId, request.BookId, cancellationToken))
        {
            throw new BusinessLogicException($"Book {request.BookId} is already owned by user {request.UserId}.");
        }

        if (await _bookRelationshipRepository.ExistsAsync<WantedBook>(request.UserId, request.BookId, cancellationToken))
        {
            throw new BusinessLogicException($"Book {request.BookId} is already wanted by user {request.UserId}.");
        }

        var newOwnedBook = new OwnedBook
        {
            UserId = request.UserId,
            BookId = request.BookId,
            BookStateId = request.BookStateId,
            AddedDate = DateTime.UtcNow
        };

        _repository.Add<OwnedBook>(newOwnedBook);

        await _repository.SaveAsync(cancellationToken);
    }
}