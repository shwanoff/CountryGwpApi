using CountryGwp.Domain.Interfaces;
using CountryGwp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CountryGwp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, string csvFilePath)
	{
		var records = Services.CsvLoader.Load(csvFilePath);
		services.AddSingleton<IGwpRepository>(new GwpRepository(records));
		return services;
	}
}