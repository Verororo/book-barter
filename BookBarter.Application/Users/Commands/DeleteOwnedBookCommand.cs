using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record DeleteOwnedBookCommand(int id) : IRequest;
public class DeleteOwnedBookHandler : IRequestHandler<DeleteOwnedBookCommand>
{
    private readonly IRepository<OwnedBook> _ownedBookRepository;
    public DeleteOwnedBookHandler(IRepository<OwnedBook> ownedBookRepository)
    {
        _ownedBookRepository = ownedBookRepository;
    }
    public async Task Handle(DeleteOwnedBookCommand request, CancellationToken cancellationToken)
    {
        var ownedBook = await _ownedBookRepository.GetByIdAsync(request.id);
        if (ownedBook == null)
        {
            throw new EntityNotFoundException($"No user owning book pair with id {request.id} found");
        }

        _ownedBookRepository.Delete(ownedBook);
        await _ownedBookRepository.SaveAsync();
    }
}
