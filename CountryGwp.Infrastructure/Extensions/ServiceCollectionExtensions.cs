using CountryGwp.Domain.Interfaces;
using CountryGwp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CountryGwp.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for registering infrastructure layer services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers infrastructure services, including the GWP repository loaded from a CSV file, for dependency injection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="csvFilePath">The file path to the CSV data source.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string csvFilePath)
    {
        var records = Services.CsvLoader.Load(csvFilePath);
        services.AddSingleton<IGwpRepository>(new GwpRepository(records));
        return services;
    }
}