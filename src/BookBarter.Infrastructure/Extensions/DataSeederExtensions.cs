using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.DataSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookBarter.Infrastructure.Extensions;

public static class DataSeederExtensions
{
    public static async Task SeedDataAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            await SeedFacade.SeedData(context, userManager);
        }
    }
}
