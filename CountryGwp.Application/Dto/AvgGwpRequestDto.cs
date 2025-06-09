namespace CountryGwp.Application.Dto;

public class AvgGwpRequestDto
{
	public required string Country { get; set; }
	public required IEnumerable<string> Lobs { get; set; }
	public required int FromYear { get; set; }
	public required int ToYear { get; set; }
}
