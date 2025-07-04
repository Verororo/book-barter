using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.DataSeed;

public class BookRelationshipsSeed
{
    public static async Task Seed(UserManager<User> userManager, AppDbContext context)
    {
        if (context.OwnedBooks.Any() || context.WantedBooks.Any()) return;

        var alexei = context.Users.First(u => u.UserName == "Alexei");
        await context.AddRangeAsync([
            new OwnedBook {
                User = alexei,
                Book = context.Books.First(b => b.Title == "War and Peace"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            },
            new OwnedBook {
                User = alexei,
                Book = context.Books.First(b => b.Title == "Tractatus Logico-Philosophicus"),
                BookStateId = 2,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = alexei,
                Book = context.Books.First(b => b.Title == "Les Miserables"),
                AddedDate = DateTime.Now
            }
        ]);

        var anna = context.Users.First(u => u.UserName == "Anna");
        await context.AddRangeAsync([
            new OwnedBook {
                User = anna,
                Book = context.Books.First(b => b.Title == "The Fellowship of The Ring"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = anna,
                Book = context.Books.First(b => b.Title == "War and Peace"),
                AddedDate = DateTime.Now
            }
        ]);

        var vasily = context.Users.First(u => u.UserName == "Vasily");
        await context.AddRangeAsync([
            new OwnedBook {
                User = vasily,
                Book = context.Books.First(b => b.Title == "Atlas Shrugged"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = vasily,
                Book = context.Books.First(b => b.Title == "Les Miserables"),
                AddedDate = DateTime.Now
            }
        ]);

        await context.SaveChangesAsync();
    }
}
