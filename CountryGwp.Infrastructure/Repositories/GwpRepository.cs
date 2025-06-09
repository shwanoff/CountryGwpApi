using CountryGwp.Domain.Entities;
using CountryGwp.Domain.Interfaces;

namespace CountryGwp.Infrastructure.Repositories;

/// <summary>
/// Provides an in-memory implementation of the IGwpRepository interface for accessing gross written premium (GWP) records.
/// This repository loads all records into memory and allows querying by country and lines of business.
/// </summary>
public class GwpRepository : IGwpRepository
{
    private readonly List<GwpRecord> _records;

    /// <summary>
    /// Initializes a new instance of the <see cref="GwpRepository"/> class with the specified GWP records.
    /// </summary>
    /// <param name="records">The collection of GWP records to store in memory.</param>
    public GwpRepository(IEnumerable<GwpRecord> records)
    {
        _records = records.ToList();
    }

    /// <summary>
    /// Asynchronously retrieves GWP records for the specified country and lines of business.
    /// </summary>
    /// <param name="country">The country code to filter records by.</param>
    /// <param name="lobs">The collection of lines of business to filter records by.</param>
    /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of matching GWP records.
    /// </returns>
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
