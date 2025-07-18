using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Interfaces;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands.OwnedBooks;

public class AddOwnedBookCommand : IRequest
{
    public int BookId { get; set; }
    public int BookStateId { get; set; }
}

public class AddOwnedBookCommandHandler : IRequestHandler<AddOwnedBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IBookRelationshipRepository _bookRelationshipRepository;
    private readonly IEntityExistenceValidator _existenceValidator;
    private readonly ICurrentUserProvider _currentUserProvider;
    public AddOwnedBookCommandHandler(IGenericRepository repository,
        IBookRelationshipRepository bookRelationshipRepository,
        IEntityExistenceValidator existenceValidator,
        ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _bookRelationshipRepository = bookRelationshipRepository;
        _existenceValidator = existenceValidator;
        _currentUserProvider = currentUserProvider;
    }
    public async Task Handle(AddOwnedBookCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserProvider.UserId.HasValue)
        {
            throw new BusinessLogicException($"Failed to get user id from the current user provider. The method may have been called without authentication.");
        }
        var userId = _currentUserProvider.UserId.Value;

        await _existenceValidator.ValidateAsync<User>(userId, cancellationToken);
        await _existenceValidator.ValidateAsync<Book>(request.BookId, cancellationToken);
        await _existenceValidator.ValidateAsync<BookState>(request.BookStateId, cancellationToken);

        if (await _bookRelationshipRepository.ExistsAsync<OwnedBook>(userId, request.BookId, cancellationToken))
        {
            throw new BusinessLogicException($"Book {request.BookId} is already owned by user {userId}.");
        }

        if (await _bookRelationshipRepository.ExistsAsync<WantedBook>(userId, request.BookId, cancellationToken))
        {
            throw new BusinessLogicException($"Book {request.BookId} is already wanted by user {userId}.");
        }

        var newOwnedBook = new OwnedBook
        {
            UserId = userId,
            BookId = request.BookId,
            BookStateId = request.BookStateId,
            AddedDate = DateTime.UtcNow
        };

        _repository.Add(newOwnedBook);

        await _repository.SaveAsync(cancellationToken);
    }
}