using BookBarter.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookBarter.Infrastructure.DataSeed;

public class UsersSeed
{
    public static async Task Seed(UserManager<User> userManager, AppDbContext context)
    {
        if (userManager.Users.Any()) return;

        var moderator = new User
        {
            UserName = "Moderator",
            Email = "moderator@outlook.com",
            City = context.Cities.First(c => c.NameAscii == "Chisinau"),
            RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
            LastOnlineDate = DateTime.Parse("2025-07-03 15:00")
        };

        await userManager.CreateAsync(moderator, "P@ssW0rd");
        await userManager.AddToRoleAsync(moderator, "Moderator");


        await userManager.CreateAsync(
            new User
            {
                UserName = "Alexei",
                Email = "alexei@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Chisinau"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-07-03 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Alexandr",
                Email = "alexandr@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Kyiv"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-07-02 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Anna",
                Email = "anna@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Chisinau"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-07-01 15:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Maria",
                Email = "maria@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Chisinau"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-07-01 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Oleg",
                Email = "oleg@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Odesa"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-07-01 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Nikolai",
                Email = "nikolai@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Moscow"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-06-30 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Vasily",
                Email = "vasily@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Chisinau"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-06-29 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Arina",
                Email = "arina@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Sofia"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-06-28 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Sofia",
                Email = "sofia@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Bucharest"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-06-27 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Mihai",
                Email = "mihai@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Bucharest"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-06-26 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Jan",
                Email = "jan@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Chisinau"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-06-25 00:00")
            },
            "P@ssW0rd"
        );

        await userManager.CreateAsync(
            new User
            {
                UserName = "Denis",
                Email = "denis@outlook.com",
                City = context.Cities.First(c => c.NameAscii == "Sofia"),
                RegistrationDate = DateTime.Parse("2025-01-01 00:00"),
                LastOnlineDate = DateTime.Parse("2025-06-24 00:00")
            },
            "P@ssW0rd"
        );
    }
}
