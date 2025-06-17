using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public class GetByIdBookQuery : IRequest<BookDto>
{
    public int Id { get; set; }
}

public class GetByIdBookQueryHandler : IRequestHandler<GetByIdBookQuery, BookDto>
{
    private readonly IBookRepository _bookRepository;

    public GetByIdBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> Handle(GetByIdBookQuery request, CancellationToken cancellationToken)
    {
        var bookDto = await _bookRepository.GetDtoByIdAsync(request.Id, cancellationToken);
        if (bookDto == null) throw new EntityNotFoundException(typeof(Book).Name, request.Id);

        return bookDto;
    }
}
