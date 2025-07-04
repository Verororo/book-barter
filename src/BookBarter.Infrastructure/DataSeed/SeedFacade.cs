using BookBarter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.DataSeed;

public class SeedFacade
{
    public static async Task SeedData(AppDbContext appDbContext, UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        appDbContext.Database.Migrate();

        await RolesSeed.Seed(roleManager);

        await CitiesSeed.Seed(appDbContext);
        await AuthorsSeed.Seed(appDbContext);
        await GenreSeed.Seed(appDbContext);
        await PublisherSeed.Seed(appDbContext);
        await BooksSeed.Seed(appDbContext);
        await UsersSeed.Seed(userManager, appDbContext);
        await BookRelationshipsSeed.Seed(userManager, appDbContext);
    }
}