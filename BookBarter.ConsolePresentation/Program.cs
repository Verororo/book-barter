
using BookBarter.Application.Abstractions;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Users.Commands;
using BookBarter.Application.Users.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


class Program
{
    static async Task Main()
    {
        var diContainer = new ServiceCollection()
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IBookRepository, BookRepository>()
            .AddSingleton<IOwnedBookRepository, OwnedBookRepository>()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IUserRepository).Assembly))
            .BuildServiceProvider();

        var mediator = diContainer.GetRequiredService<IMediator>();

        var user1 = await mediator.Send(new CreateUser("Vasya", "vasya@example.com", "Chisinau"));
        var user2 = await mediator.Send(new CreateUser("Anton", "anton@example.com", "Chisinau"));
        var user3 = await mediator.Send(new CreateUser("Maxim", "maxim@example.com", "Chisinau"));

        var book1 = await mediator.Send(new CreateBook("9780132350884", "Clean Code", 0));
        var book2 = await mediator.Send(new CreateBook("9780201633610", "Design Patterns", 0));

        // Vasya has Clean Code
        var userBook1 = await mediator.Send(new CreateOwnedBook(user1.Id, book1.Id, 0));
        // Vasya has Design Patterns
        var userBook2 = await mediator.Send(new CreateOwnedBook(user1.Id, book2.Id, 0));

        Console.WriteLine("Vasya's owned books:");
        var vasyasBooks = await mediator.Send(new GetByPredicateOwnedBooks(x => x.UserId == user1.Id));
        foreach (var userBook in vasyasBooks)
        {
            var book = await mediator.Send(new GetByIdBook(userBook.BookId));
            Console.WriteLine($"{book.Title}, {book.GenreId}");
        }
    }
}
