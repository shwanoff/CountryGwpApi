namespace CountryGwp.Application.Dto;

/// <summary>
/// Represents the response for the average gross written premium (GWP) calculation.
/// The dictionary maps each line of business (LOB) to its calculated average GWP value.
/// </summary>
public class AvgGwpResponseDto : Dictionary<string, decimal>
{
}
