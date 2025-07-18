
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands.WantedBooks;

public class AddWantedBookCommand : IRequest
{
    public int BookId { get; set; }
}

public class AddWantedBookCommandHandler : IRequestHandler<AddWantedBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IBookRelationshipRepository _bookRelationshipRepository;
    private readonly IEntityExistenceValidator _existenceValidator;
    private readonly ICurrentUserProvider _currentUserProvider;
    public AddWantedBookCommandHandler(IGenericRepository repository,
        IBookRelationshipRepository bookRelationshipRepository,
        IEntityExistenceValidator existenceValidator,
        ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _bookRelationshipRepository = bookRelationshipRepository;
        _existenceValidator = existenceValidator;
        _currentUserProvider = currentUserProvider;
    }
    public async Task Handle(AddWantedBookCommand request, CancellationToken cancellationToken)
    {
        var userId = (int)_currentUserProvider.UserId!;  // FIX: add null check

        await _existenceValidator.ValidateAsync<User>(userId, cancellationToken);
        await _existenceValidator.ValidateAsync<Book>(request.BookId, cancellationToken);

        if (await _bookRelationshipRepository.ExistsAsync<OwnedBook>(userId, request.BookId, cancellationToken))
        {
            throw new BusinessLogicException($"Book {request.BookId} is already owned by user {userId}.");
        }

        if (await _bookRelationshipRepository.ExistsAsync<WantedBook>(userId, request.BookId, cancellationToken))
        {
            throw new BusinessLogicException($"Book {request.BookId} is already wanted by user {userId}.");
        }

        var newWantedBook = new WantedBook
        {
            UserId = userId,
            BookId = request.BookId,
            AddedDate = DateTime.UtcNow
        };

        _repository.Add<WantedBook>(newWantedBook);

        await _repository.SaveAsync(cancellationToken);
    }
}
