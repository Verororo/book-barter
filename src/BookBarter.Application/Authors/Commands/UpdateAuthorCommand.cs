using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Authors.Commands;

public class UpdateAuthorCommand : IRequest
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
}
public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IGenericRepository _repository;
    public UpdateAuthorCommandHandler(
        IGenericRepository repository
        )
    {
        _repository = repository;
    }
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync<Author>(request.Id, cancellationToken);
        if (author == null) throw new EntityNotFoundException(typeof(Author).Name, request.Id);  // FIX: use validator

        author.FirstName = request.FirstName;
        author.MiddleName = request.MiddleName;
        author.LastName = request.LastName;

        await _repository.SaveAsync(cancellationToken);
    }
}