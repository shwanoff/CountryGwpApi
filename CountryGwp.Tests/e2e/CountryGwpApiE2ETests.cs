using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CountryGwp.Application.Dto;
using CountryGwpApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CountryGwp.Tests.E2E;

[TestFixture]
public class CountryGwpApiE2ETests
{
	private WebApplicationFactory<Program> _factory = null!;
	private HttpClient _client = null!;

	[SetUp]
	public void SetUp()
	{
		_factory = new WebApplicationFactory<Program>();
		_client = _factory.CreateClient(new WebApplicationFactoryClientOptions
		{
			BaseAddress = new Uri("http://localhost:9091")
		});
	}

	[TearDown]
	public void TearDown()
	{
		_client.Dispose();
		_factory.Dispose();
	}

	[Test]
	public async Task Post_AvgGwp_ReturnsExpectedResult()
	{
		// Arrange
		var request = new AvgGwpRequestDto
		{
			Country = "ae",
			Lob = ["property"],
			FromYear = 2010,
			ToYear = 2011
		};

		// Act
		var response = await _client.PostAsJsonAsync("/server/api/gwp/avg", request);

		// Assert
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		var json = await response.Content.ReadAsStringAsync();
		var result = JsonSerializer.Deserialize<Dictionary<string, decimal>>(json);

		Assert.That(result, Is.Not.Null);
		Assert.That(result!.ContainsKey("property"), Is.True);
		Assert.That(result["property"], Is.EqualTo(650914908.1m).Within(0.1m)); 
	}

	[Test]
	public async Task Post_AvgGwp_ReturnsBadRequest_OnInvalidRequest()
	{
		// Arrange
		var request = new { };

		// Act
		var response = await _client.PostAsJsonAsync("/server/api/gwp/avg", request);

		// Assert
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
	}
}