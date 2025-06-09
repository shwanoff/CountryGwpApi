using CountryGwp.Domain.Entities;

namespace CountryGwp.Domain.Interfaces;

public interface IGwpRepository
{
	Task<IEnumerable<GwpRecord>> GetByCountryAndLobsAsync(string country, IEnumerable<string> lobs, CancellationToken cancellationToken = default);
}
