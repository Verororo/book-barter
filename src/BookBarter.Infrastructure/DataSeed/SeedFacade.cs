using BookBarter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.DataSeed;

public class SeedFacade
{
    public static async Task SeedData(AppDbContext appDbContext, UserManager<User> userManager)
    {
        appDbContext.Database.Migrate();

        await CitiesSeed.Seed(appDbContext);
    }
}