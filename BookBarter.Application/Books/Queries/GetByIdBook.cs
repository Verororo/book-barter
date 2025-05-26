
using BookBarter.Application.Abstractions;
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public record GetByIdBook(int id) : IRequest<BookDto>;

public class GetByIdBookHandler : IRequestHandler<GetByIdBook, BookDto>
{
    private readonly IBookRepository _bookRepository;

    public GetByIdBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<BookDto> Handle(GetByIdBook request, CancellationToken cancellationToken)
    {
        var Book = _bookRepository.GetById(request.id);
        if (Book == null)
        {
            throw new RepoMemberAbsentException($"Book with id {request.id} has not been found");
        }
        return Task.FromResult(BookDto.FromBook(Book));
    }
}
