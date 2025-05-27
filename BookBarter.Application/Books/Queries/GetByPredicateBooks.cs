
using BookBarter.Application.Abstractions;
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public record GetByPredicateBooks(Func<Book, bool> predicate) : IRequest<List<BookDto>>;

public class GetByPredicateBooksHandler : IRequestHandler<GetByPredicateBooks, List<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetByPredicateBooksHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<List<BookDto>> Handle(GetByPredicateBooks request, CancellationToken cancellationToken)
    {
        var BookList = _bookRepository.GetByPredicate(request.predicate);
        return Task.FromResult(BookList.Select(BookDto.FromBook).ToList());
    }
}
