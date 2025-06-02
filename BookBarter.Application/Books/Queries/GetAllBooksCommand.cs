
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public record GetAllBooksCommand() : IRequest<List<Book>>;

public class GetAllBooksHandler : IRequestHandler<GetAllBooksCommand, List<Book>>
{
    private readonly IReadingRepository<Book> _bookRepository;

    public GetAllBooksHandler(IReadingRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<List<Book>> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
    {
        return _bookRepository.GetAllAsync(x => x.Authors, x => x.Genre);
    }
}
