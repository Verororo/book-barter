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
    public DeleteAuthorCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync<Author>(request.Id, cancellationToken, a => a.Books);
        if (author == null) throw new EntityNotFoundException(typeof(Author).Name, request.Id);

        foreach (var book in author.Books)
        {
            _repository.Delete<Book>(book);
        }

        _repository.Delete<Author>(author);
        await _repository.SaveAsync(cancellationToken);
    }
}
