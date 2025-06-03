
using System.Linq.Expressions;
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetByPredicateUsersQuery(Expression<Func<User, bool>> predicate) : IRequest<List<User>>;

public class GetByPredicateUsersHandler : IRequestHandler<GetByPredicateUsersQuery, List<User>>
{
    private readonly IReadingRepository<User> _userRepository;

    public GetByPredicateUsersHandler(IReadingRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> Handle(GetByPredicateUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByPredicateAsync(request.predicate);
    }
}
