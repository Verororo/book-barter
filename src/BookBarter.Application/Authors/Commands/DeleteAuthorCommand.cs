using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Authors.Commands;

public class DeleteAuthorCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public DeleteAuthorCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }
    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync<Author>(request.Id, cancellationToken, a => a.Books);
        _entityExistenceValidator.ValidateAsync(author, request.Id);

        if (author.Books != null && author.Books.Count != 0)
        {
            var bookIds = string.Join(", ", author.Books.Select(b => b.Id));

            var message = $"Attempted to delete Author {author.Id} referred by existing Books: {bookIds}.";
            throw new BusinessLogicException(message);
        }

        _repository.Delete(author);
        await _repository.SaveAsync(cancellationToken);
    }
}
