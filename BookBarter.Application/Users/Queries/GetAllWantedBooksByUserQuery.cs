using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetAllWantedBooksByUserQuery(int userId) : IRequest<List<Book>>;

public class GetAllWantedBooksByUserHandler : IRequestHandler<GetAllWantedBooksByUserQuery, List<Book>>
{
    private readonly IReadingRepository<User> _userRepository;

    public GetAllWantedBooksByUserHandler(IReadingRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<Book>> Handle(GetAllWantedBooksByUserQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId, x => x.WantedBooks,
            x => x.WantedBooks.Authors);
        // TODO: create specific repository for User
        if (user == null)
        {
            throw new EntityNotFoundException($"User not found by id: {request.userId}");
        }

        return (List<Book>)user.WantedBooks;
    }
}
