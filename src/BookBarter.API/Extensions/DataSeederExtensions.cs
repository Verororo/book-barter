using BookBarter.Infrastructure.Extensions;

namespace BookBarter.API.Extensions;

public static class DataSeederExtensions
{
    public static async Task SeedDataApiAsync(this IHost host)
    {
        await host.SeedDataAsync();
    }
}
