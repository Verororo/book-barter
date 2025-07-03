using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;

namespace BookBarter.API.Extensions;

public static class DataSeederExtensions
{
    public static async Task SeedDataApiAsync(this IHost host)
    {
        await host.SeedDataAsync();
    }
}
