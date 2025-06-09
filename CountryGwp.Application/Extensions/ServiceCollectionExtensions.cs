using CountryGwp.Application.Interfaces;
using CountryGwp.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace CountryGwp.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<ICalculateAvgGwpUseCase, CalculateAvgGwpUseCase>();
		return services;
	}
}