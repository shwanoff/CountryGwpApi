using CountryGwp.Application.Dto;

namespace CountryGwp.Application.Interfaces;

/// <summary>
/// Defines a use case for calculating the average gross written premium (GWP) for a given country,
/// a set of lines of business, and a specified year range.
/// </summary>
public interface ICalculateAvgGwpUseCase
{
    /// <summary>
    /// Calculates the average GWP for the specified country, lines of business, and year range.
    /// </summary>
    /// <param name="request">The request containing country code, lines of business, and year range.</param>
    /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a dictionary
    /// mapping each line of business to its calculated average GWP value.
    /// </returns>
    Task<AvgGwpResponseDto> HandleAsync(AvgGwpRequestDto request, CancellationToken cancellationToken = default);
}
