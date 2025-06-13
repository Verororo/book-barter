using Microsoft.AspNetCore.Identity;
public static class RoleSeederExtensions
{
    public static async Task SeedRolesApiAsync(this WebApplication app, params string[] roleNames)
    {
        await app.Services.SeedRolesInfrastructureAsync(roleNames);
    }
}