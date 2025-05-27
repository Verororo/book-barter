using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetAllOwnedBooks() : IRequest<List<OwnedBookDto>>;

public class GetAllUserBooksHandler : IRequestHandler<GetAllOwnedBooks, List<OwnedBookDto>>
{
    private readonly IOwnedBookRepository _userBookRepository;

    public GetAllUserBooksHandler(IOwnedBookRepository userBookRepository)
    {
        _userBookRepository = userBookRepository;
    }

    public Task<List<OwnedBookDto>> Handle(GetAllOwnedBooks request, CancellationToken cancellationToken)
    {
        var userBookList = _userBookRepository.GetAll();
        return Task.FromResult(userBookList.Select(OwnedBookDto.FromUserBook).ToList());
    }
}
