
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public record GetByIdBookCommand(int id) : IRequest<Book>;

public class GetByIdBookHandler : IRequestHandler<GetByIdBookCommand, Book>
{
    private readonly IReadingRepository<Book> _bookRepository;

    public GetByIdBookHandler(IReadingRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book> Handle(GetByIdBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.id, x => x.Authors, x => x.Genre);
        if (book == null)
        {
            throw new EntityNotFoundException($"Book with id {request.id} has not been found");
        }
        return book;
    }
}
