using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetAllOwnedBooksByUserQuery(int userId) : IRequest<List<OwnedBook>>;

public class GetAllOwnedBooksByUserHandler : IRequestHandler<GetAllOwnedBooksByUserQuery, List<OwnedBook>>
{
    private readonly IReadingRepository<OwnedBook> _ownedBookRepository;
    private readonly IReadingRepository<User> _userRepository;

    public GetAllOwnedBooksByUserHandler(IReadingRepository<OwnedBook> ownedBookRepository,
        IReadingRepository<User> userRepository)
    {
        _ownedBookRepository = ownedBookRepository;
        _userRepository = userRepository;
    }

    public async Task<List<OwnedBook>> Handle(GetAllOwnedBooksByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user == null)
        {
            throw new EntityNotFoundException($"User not found by id: {request.userId}");
        }
        return await _ownedBookRepository.GetByPredicateAsync(ob => ob.UserId == request.userId, 
            x => x.BookState, x => x.Book, x => x.Book.Authors);
    }
}
