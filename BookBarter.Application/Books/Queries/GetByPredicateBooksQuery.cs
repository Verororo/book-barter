
using System.Linq.Expressions;
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public record GetByPredicateBooksQuery(Expression<Func<Book, bool>> predicate) : IRequest<List<Book>>;

public class GetByPredicateBooksHandler : IRequestHandler<GetByPredicateBooksQuery, List<Book>>
{
    private readonly IReadingRepository<Book> _bookRepository;

    public GetByPredicateBooksHandler(IReadingRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<Book>> Handle(GetByPredicateBooksQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetByPredicateAsync(request.predicate, x => x.Authors, x => x.Genre);
    }
}
