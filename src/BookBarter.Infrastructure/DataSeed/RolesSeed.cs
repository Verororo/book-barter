using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookBarter.Infrastructure.DataSeed;

public class RolesSeed
{
    public static async Task Seed(RoleManager<IdentityRole<int>> roleManager)
    {
        if (roleManager.Roles.Any()) return;

        IEnumerable<string> roleNames = ["Moderator"];
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
    }
}