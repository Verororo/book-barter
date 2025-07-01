
using BookBarter.Application.Auth.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Auth;
using BookBarter.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookBarter.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository), typeof(GenericRepository));
        services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

        services.AddIdentity<User, IdentityRole<int>>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}
