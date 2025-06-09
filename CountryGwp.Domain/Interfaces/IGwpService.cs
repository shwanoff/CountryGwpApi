namespace CountryGwp.Domain.Interfaces;

public interface IGwpService
{
	Task<IDictionary<string, double>> GetAverageGwpAsync(string country, IEnumerable<string> lobs, CancellationToken cancellationToken = default);
}
