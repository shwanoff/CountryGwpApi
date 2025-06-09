using CountryGwp.Application.Dto;
using CountryGwp.Application.UseCases;
using CountryGwp.Domain.Entities;
using CountryGwp.Domain.Interfaces;
using CountryGwp.Domain.ValueObjects;
using Moq;

namespace CountryGwp.Tests.Application;

[TestFixture]
public class CalculateAvgGwpUseCaseTests
{
	[Test]
	public async Task HandleAsync_ReturnsCorrectAverage_ForValidInput()
	{
		// Arrange
		var mockRepo = new Mock<IGwpRepository>();
		var records = new List<GwpRecord>
		{
			new() {
				Country = new CountryCode("ae"),
				LineOfBusiness = new LineOfBusiness("property"),
				VariableId = new Variable("gwp"),
				YearlyGwp = new YearlyGwp(new Dictionary<string, decimal?>
				{
					{ "Y2010", 100m },
					{ "Y2011", 200m },
					{ "Y2012", 300m }
				})
			},
			new() {
				Country = new CountryCode("ae"),
				LineOfBusiness = new LineOfBusiness("property"),
				VariableId = new Variable("gwp"),
				YearlyGwp = new YearlyGwp(new Dictionary<string, decimal?>
				{
					{ "Y2010", 400m },
					{ "Y2011", null },
					{ "Y2012", 600m }
				})
			}
		};
		mockRepo.Setup(r => r.GetByCountryAndLobsAsync("ae", It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(records);

		var useCase = new CalculateAvgGwpUseCase(mockRepo.Object);
		var request = new AvgGwpRequestDto
		{
			Country = "ae",
			Lob = ["property"],
			FromYear = 2010,
			ToYear = 2012
		};

		// Act
		var result = await useCase.HandleAsync(request);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.ContainsKey("property"), Is.True);
		// (100+200+300+400+600) / 5 = 320.0
		Assert.That(result["property"], Is.EqualTo(320.0m).Within(0.1m));
	}

	[Test]
	public async Task HandleAsync_ReturnsZero_WhenNoDataForLob()
	{
		// Arrange
		var mockRepo = new Mock<IGwpRepository>();
		mockRepo.Setup(r => r.GetByCountryAndLobsAsync("ae", It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync([]);

		var useCase = new CalculateAvgGwpUseCase(mockRepo.Object);
		var request = new AvgGwpRequestDto
		{
			Country = "ae",
			Lob = ["transport"],
			FromYear = 2010,
			ToYear = 2012
		};

		// Act
		var result = await useCase.HandleAsync(request);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.ContainsKey("transport"), Is.True);
		Assert.That(result["transport"], Is.EqualTo(0.0m));
	}

	[Test]
	public void HandleAsync_ThrowsArgumentNullException_WhenRequestIsNull()
	{
		// Arrange
		var mockRepo = new Mock<IGwpRepository>();
		var useCase = new CalculateAvgGwpUseCase(mockRepo.Object);

		// Act & Assert
		Assert.That(
			async () => await useCase.HandleAsync(null!, CancellationToken.None),
			Throws.ArgumentNullException);
	}
}