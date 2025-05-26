
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.BookBooks.Commands;
public record DeleteBook(int id) : IRequest<Unit>;
public class DeleteBookHandler : IRequestHandler<DeleteBook, Unit>
{
    private readonly IBookRepository _bookRepository;
    public DeleteBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public Task<Unit> Handle(DeleteBook request, CancellationToken cancellationToken)
    {
        var Book = _bookRepository.GetById(request.id);
        if (Book == null)
        {
            throw new RepoMemberAbsentException($"Book with id {request.id} has not been found");
        }
        _bookRepository.Delete(Book);
        return Task.FromResult(Unit.Value);
    }
}
