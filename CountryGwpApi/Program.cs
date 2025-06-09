using CountryGwp.Application.Extensions;
using CountryGwp.Infrastructure.Extensions;
using Scalar.AspNetCore;

namespace CountryGwpApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

		builder.WebHost.UseUrls("http://*:9091");

		builder.Services.AddControllers();
        builder.Services.AddOpenApi();

		var csvPath = Path.Combine(AppContext.BaseDirectory, "Data", "gwpByCountry.csv");
		builder.Services.AddInfrastructure(csvPath);
		builder.Services.AddApplication();

		var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
			app.MapScalarApiReference();
		}

        app.UseHttpsRedirection();
        app.UseAuthorization();


        app.MapControllers();
        app.Run();
    }
}
