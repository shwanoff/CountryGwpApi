using CountryGwp.Application.Dto;
using CountryGwp.Application.Interfaces;
using CountryGwp.Domain.Interfaces;

namespace CountryGwp.Application.UseCases;

/// <summary>
/// Implements the use case for calculating the average gross written premium (GWP) for a specified country,
/// a set of lines of business, and a given year range.
/// </summary>
public class CalculateAvgGwpUseCase(IGwpRepository repository) : ICalculateAvgGwpUseCase
{
    private readonly IGwpRepository _repository = repository;

    /// <summary>
    /// Handles the calculation of the average GWP for the provided request parameters.
    /// </summary>
    /// <param name="request">The request containing country code, lines of business, and year range.</param>
    /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a dictionary
    /// mapping each line of business to its calculated average GWP value.
    /// </returns>
    public async Task<AvgGwpResponseDto> HandleAsync(AvgGwpRequestDto request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var records = await _repository.GetByCountryAndLobsAsync(request.Country, request.Lob, cancellationToken);

        var result = new AvgGwpResponseDto();

        foreach (var lob in request.Lob)
        {
            var avg = records
                .Where(r => r.LineOfBusiness.Value == lob)
                .SelectMany(r => r.YearlyGwp.Values
                    .Where(y =>
                    {
                        // TODO: should change type from string to int in YearlyGwp.Values
                        if (y.Key.Length == 5 && y.Key.StartsWith("Y") && int.TryParse(y.Key[1..], out var year))
                        {
                            return year >= request.FromYear && year <= request.ToYear;
                        }
                        return false;
                    })
                    .Select(y => y.Value))
                .Where(v => v.HasValue)
                .Select(v => v!.Value)
                .DefaultIfEmpty(0)
                .Average();

            result[lob] = Math.Round(avg, 1);
        }

        return result;
    }
}
