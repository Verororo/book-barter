
using System.Reflection;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using BookBarter.Application.Common.Behaviors;
using BookBarter.Application.Common.Queries;

namespace BookBarter.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddScoped<IEntityExistenceValidator, EntityExistenceValidator>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IValidator<>), typeof(PagedQueryValidator<>));
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
