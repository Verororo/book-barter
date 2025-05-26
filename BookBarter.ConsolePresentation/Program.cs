
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
            .AddSingleton<IUserHasBookRepository, UserBookRepository>()
            .AddSingleton<IUserWantsBookRepository, WantedUserBookRepository>()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IUserRepository).Assembly))
            .BuildServiceProvider();

        var mediator = diContainer.GetRequiredService<IMediator>();

        var user1 = await mediator.Send(new CreateUser("Vasya", "vasya@example.com", "Chisinau"));
        var user2 = await mediator.Send(new CreateUser("Anton", "anton@example.com", "Chisinau"));
        var user3 = await mediator.Send(new CreateUser("Maxim", "maxim@example.com", "Chisinau"));

        var book1 = await mediator.Send(new CreateBook("9780132350884", "Clean Code", "Applied Skills"));
        var book2 = await mediator.Send(new CreateBook("9780201633610", "Design Patterns", "Applied Skills"));

        // Vasya has Clean Code
        var userBook1 = await mediator.Send(new CreateUserHasBook(user1.Id, book1.Id, BookBarter.Domain.Enums.State.New));
        // Vasya has Design Patterns
        var userBook2 = await mediator.Send(new CreateUserHasBook(user1.Id, book2.Id, BookBarter.Domain.Enums.State.New));

        // Anton wants Clean Code
        var wantedUserBook1 = await mediator.Send(new CreateUserWantsBook(user2.Id, book1.Id));
        // Maxim wants Design Patterns
        var wantedUserBook2 = await mediator.Send(new CreateUserWantsBook(user3.Id, book2.Id));

        Console.WriteLine("Vasya's owned books:");
        var vasyasBooks = await mediator.Send(new GetByPredicateUserHasBooks(x => x.UserId == user1.Id));
        foreach (var userBook in vasyasBooks)
        {
            var book = await mediator.Send(new GetByIdBook(userBook.BookId));
            Console.WriteLine($"{book.Title}, {book.Genre}");
        }

        Console.WriteLine("Anton's wanted books:");
        var antonsWantedBooks = await mediator.Send(new GetByPredicateUserWantsBooks(x => x.UserId == user2.Id));
        foreach (var wantedUserBook in antonsWantedBooks)
        {
            var book = await mediator.Send(new GetByIdBook(wantedUserBook.BookId));
            Console.WriteLine($"{book.Title}, {book.Genre}");
        }
    }
}
