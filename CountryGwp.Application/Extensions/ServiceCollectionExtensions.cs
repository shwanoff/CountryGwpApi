using CountryGwp.Application.Interfaces;
using CountryGwp.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace CountryGwp.Application.Extensions;

/// <summary>
/// Provides extension methods for registering application layer services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application use cases and related services for dependency injection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICalculateAvgGwpUseCase, CalculateAvgGwpUseCase>();
        return services;
    }
}