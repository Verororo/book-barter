
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Commands.WantedBooks;

public class AddWantedBookCommand : IRequest
{
    public int UserId { get; set; }
    public int BookId { get; set; }
}

public class AddWantedBookCommandHandler : IRequestHandler<AddWantedBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IBookRelationshipRepository _bookRelationshipRepository;
    private readonly IEntityExistenceValidator _existenceValidator;
    public AddWantedBookCommandHandler(IGenericRepository repository,
        IBookRelationshipRepository bookRelationshipRepository,
        IEntityExistenceValidator existenceValidator)
    {
        _repository = repository;
        _bookRelationshipRepository = bookRelationshipRepository;
        _existenceValidator = existenceValidator;
    }
    public async Task Handle(AddWantedBookCommand request, CancellationToken cancellationToken)
    {
        await _existenceValidator.ValidateAsync<User>(request.UserId, cancellationToken);
        await _existenceValidator.ValidateAsync<Book>(request.BookId, cancellationToken);

        if (await _bookRelationshipRepository.ExistsAsync<OwnedBook>(request.UserId, request.BookId, cancellationToken))
        {
            throw new Exception($"Book {request.BookId} is already owned by user {request.UserId}.");
        }

        if (await _bookRelationshipRepository.ExistsAsync<WantedBook>(request.UserId, request.BookId, cancellationToken))
        {
            throw new Exception($"Book {request.BookId} is already wanted by user {request.UserId}.");
        }

        var newWantedBook = new WantedBook
        {
            UserId = request.UserId,
            BookId = request.BookId,
            AddedDate = DateTime.UtcNow
        };

        _repository.Add<WantedBook>(newWantedBook);

        await _repository.SaveAsync(cancellationToken);
    }
}
