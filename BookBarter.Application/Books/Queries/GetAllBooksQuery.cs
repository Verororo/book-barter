
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public record GetAllBooksQuery() : IRequest<List<Book>>;

public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
{
    private readonly IReadingRepository<Book> _bookRepository;

    public GetAllBooksHandler(IReadingRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return _bookRepository.GetAllAsync(x => x.Authors, x => x.Genre);
    }
}
