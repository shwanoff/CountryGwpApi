namespace CountryGwp.Domain.Interfaces;

/// <summary>
/// Defines a contract for caching average GWP calculation results.
/// </summary>
public interface IGwpCache
{
	bool TryGet(string key, out decimal value);
	void Set(string key, decimal value);
	void Clear();
	static string GetKey(string country, string lob, int fromYear, int toYear)
		=> $"{country.ToLowerInvariant()}|{lob.ToLowerInvariant()}|{fromYear}|{toYear}";
}