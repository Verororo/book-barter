using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Interfaces;
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
    private readonly IEntityExistenceValidator _entityExistenceValidator;

    public GetByIdBookQueryHandler(IBookRepository bookRepository, IEntityExistenceValidator entityExistenceValidator)
    {
        _bookRepository = bookRepository;
        _entityExistenceValidator = entityExistenceValidator;
    }

    public async Task<BookDto> Handle(GetByIdBookQuery request, CancellationToken cancellationToken)
    {
        var bookDto = await _bookRepository.GetDtoByIdAsync(request.Id, cancellationToken);
        _entityExistenceValidator.ValidateAsync(bookDto, request.Id);

        return bookDto;
    }
}
