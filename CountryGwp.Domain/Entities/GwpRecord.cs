using CountryGwp.Domain.ValueObjects;

namespace CountryGwp.Domain.Entities;

public class GwpRecord
{
	public required CountryCode Country { get; set; }
	public required LineOfBusiness LineOfBusiness { get; set; }
	public required Variable VariableId { get; set; }
	public required YearlyGwp YearlyGwp { get; set; }
}
