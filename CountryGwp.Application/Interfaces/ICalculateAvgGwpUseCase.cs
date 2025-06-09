using CountryGwp.Application.Dto;

namespace CountryGwp.Application.Interfaces;

public interface ICalculateAvgGwpUseCase
{
	Task<AvgGwpResponseDto> HandleAsync(AvgGwpRequestDto request, CancellationToken cancellationToken = default);

}
