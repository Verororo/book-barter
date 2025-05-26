
using BookBarter.Application.Abstractions;
using BookBarter.Application.Books.Responses;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public record GetAllBooks() : IRequest<List<BookDto>>;

public class GetAllBooksHandler : IRequestHandler<GetAllBooks, List<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<List<BookDto>> Handle(GetAllBooks request, CancellationToken cancellationToken)
    {
        var BookList = _bookRepository.GetAll();
        return Task.FromResult(BookList.Select(BookDto.FromBook).ToList());
    }
}
