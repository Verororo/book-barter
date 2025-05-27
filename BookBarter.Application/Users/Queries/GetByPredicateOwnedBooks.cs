using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetByPredicateOwnedBooks(Func<OwnedBook, bool> predicate) : IRequest<List<OwnedBookDto>>;

public class GetByPredicateUserBooksHandler : IRequestHandler<GetByPredicateOwnedBooks, List<OwnedBookDto>>
{
    private readonly IOwnedBookRepository _userBookRepository;

    public GetByPredicateUserBooksHandler(IOwnedBookRepository userBookRepository)
    {
        _userBookRepository = userBookRepository;
    }

    public Task<List<OwnedBookDto>> Handle(GetByPredicateOwnedBooks request, CancellationToken cancellationToken)
    {
        var userBookList = _userBookRepository.GetByPredicate(request.predicate);
        return Task.FromResult(userBookList.Select(OwnedBookDto.FromUserBook).ToList());
    }
}
