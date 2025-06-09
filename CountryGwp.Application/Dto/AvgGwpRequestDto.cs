namespace CountryGwp.Application.Dto;

/// <summary>
/// Data transfer object for requesting the average gross written premium (GWP) calculation.
/// Contains the country code, a list of lines of business, and the year range for the calculation.
/// </summary>
public class AvgGwpRequestDto
{
    /// <summary>
    /// The ISO country code (e.g., "ae", "us").
    /// </summary>
    public required string Country { get; set; }

    /// <summary>
    /// The collection of lines of business to include in the calculation (e.g., "property", "transport").
    /// </summary>
    public required IEnumerable<string> Lob { get; set; }

    /// <summary>
    /// The starting year (inclusive) for the average calculation.
    /// </summary>
    public int FromYear { get; set; }

    /// <summary>
    /// The ending year (inclusive) for the average calculation.
    /// </summary>
    public int ToYear { get; set; }
}
