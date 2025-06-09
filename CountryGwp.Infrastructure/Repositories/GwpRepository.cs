using CountryGwp.Domain.Entities;
using CountryGwp.Domain.Interfaces;

namespace CountryGwp.Infrastructure.Repositories;

public class GwpRepository : IGwpRepository
{
	private readonly List<GwpRecord> _records;

	public GwpRepository(IEnumerable<GwpRecord> records)
	{
		_records = records.ToList();
	}

	public Task<IEnumerable<GwpRecord>> GetByCountryAndLobsAsync(string country, IEnumerable<string> lobs, CancellationToken cancellationToken = default)
	{
		var lobsSet = new HashSet<string>(lobs, StringComparer.OrdinalIgnoreCase);
		var result = _records
			.Where(r => r.Country.Value.Equals(country, StringComparison.OrdinalIgnoreCase)
						&& lobsSet.Contains(r.LineOfBusiness.Value))
			.ToList();

		return Task.FromResult<IEnumerable<GwpRecord>>(result);
	}
}
