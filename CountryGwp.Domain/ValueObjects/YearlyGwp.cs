namespace CountryGwp.Domain.ValueObjects;

public record YearlyGwp(Dictionary<string, decimal?> Values)
{
	public decimal? this[string year] => Values.TryGetValue(year, out var value) ? value : null;
	public static YearlyGwp Empty => new YearlyGwp(new Dictionary<string, decimal?>());
	
	public bool IsEmpty => Values.Count == 0 || Values.All(v => v.Value == null);
}