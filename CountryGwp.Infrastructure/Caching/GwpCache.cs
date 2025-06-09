using System.Collections.Concurrent;
using CountryGwp.Domain.Interfaces;

namespace CountryGwp.Infrastructure.Caching;

/// <summary>
/// Provides a thread-safe in-memory cache for storing average GWP calculation results.
/// </summary>
public class GwpCache : IGwpCache
{
	private readonly ConcurrentDictionary<string, decimal> _cache = new();

	public bool TryGet(string key, out decimal value) => _cache.TryGetValue(key, out value);

	public void Set(string key, decimal value) => _cache[key] = value;

	public void Clear() => _cache.Clear();
}