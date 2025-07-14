using BookBarter.Application.BookBooks.Commands;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Publishers.Commands;

public class DeletePublisherCommand : IRequest
{
    public int Id { get; set; }
}

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
{
    private readonly IGenericRepository _repository;
    public DeletePublisherCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _repository.GetByIdAsync<Publisher>(request.Id, cancellationToken);
        if (publisher == null) throw new EntityNotFoundException(typeof(Publisher).Name, request.Id);

        _repository.Delete<Publisher>(publisher);
        await _repository.SaveAsync(cancellationToken);
    }
}
