using CountryGwp.Application.Dto;
using CountryGwp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CountryGwpApi.Controllers;

[ApiController]
[Route("server/api/gwp")]
public class CountryGwpController(ICalculateAvgGwpUseCase useCase) : ControllerBase
{
	[HttpPost("avg")]
	public async Task<IActionResult> GetAvgGwp([FromBody] AvgGwpRequestDto request, CancellationToken cancellationToken)
	{
		if (request == null || string.IsNullOrWhiteSpace(request.Country) || request.Lob == null)
			return BadRequest("Invalid request.");

		// Default values for FromYear and ToYear
		if (request.FromYear == 0)
			request.FromYear = 2008;

		if (request.ToYear == 0)
			request.ToYear = 2015;

		var result = await useCase.HandleAsync(request, cancellationToken);
		return Ok(result);
	}
}