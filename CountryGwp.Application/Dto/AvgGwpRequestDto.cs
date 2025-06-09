namespace CountryGwp.Application.Dto;

public class AvgGwpRequestDto
{
	public required string Country { get; set; }
	public required IEnumerable<string> Lob { get; set; }
	public int FromYear { get; set; }
	public int ToYear { get; set; }
}
