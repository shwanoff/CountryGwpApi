using CountryGwp.Domain.Entities;
using CountryGwp.Domain.ValueObjects;
using CountryGwp.Infrastructure.Repositories;

namespace CountryGwp.Tests.Infrastructure;

[TestFixture]
public class GwpRepositoryTests
{
	[Test]
	public async Task GetByCountryAndLobsAsync_ReturnsMatchingRecords_ByCountryAndLobs()
	{
		// Arrange
		var records = new List<GwpRecord>
		{
			new() {
				Country = new CountryCode("ae"),
				LineOfBusiness = new LineOfBusiness("property"),
				VariableId = new Variable("gwp"),
				YearlyGwp = new YearlyGwp(new Dictionary<string, decimal?> { { "Y2010", 100m } })
			},
			new() {
				Country = new CountryCode("ae"),
				LineOfBusiness = new LineOfBusiness("transport"),
				VariableId = new Variable("gwp"),
				YearlyGwp = new YearlyGwp(new Dictionary<string, decimal?> { { "Y2010", 200m } })
			},
			new() {
				Country = new CountryCode("us"),
				LineOfBusiness = new LineOfBusiness("property"),
				VariableId = new Variable("gwp"),
				YearlyGwp = new YearlyGwp(new Dictionary<string, decimal?> { { "Y2010", 300m } })
			}
		};
		var repo = new GwpRepository(records);

		// Act
		var result = await repo.GetByCountryAndLobsAsync("ae", ["property", "transport"], CancellationToken.None);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Count(), Is.EqualTo(2));
		Assert.That(result.Any(r => r.LineOfBusiness.Value == "property"), Is.True);
		Assert.That(result.Any(r => r.LineOfBusiness.Value == "transport"), Is.True);
	}

	[Test]
	public async Task GetByCountryAndLobsAsync_ReturnsEmpty_WhenNoMatch()
	{
		// Arrange
		var records = new List<GwpRecord>
		{
			new() {
				Country = new CountryCode("ae"),
				LineOfBusiness = new LineOfBusiness("property"),
				VariableId = new Variable("gwp"),
				YearlyGwp = new YearlyGwp([])
			}
		};
		var repo = new GwpRepository(records);

		// Act
		var result = await repo.GetByCountryAndLobsAsync("us", ["transport"], CancellationToken.None);

		// Assert
		Assert.That(result, Is.Empty);
	}

	[Test]
	public void GetByCountryAndLobsAsync_ThrowsArgumentException_WhenCountryIsNullOrWhiteSpace()
	{
		// Arrange
		var repo = new GwpRepository([]);

		// Act & Assert
		Assert.That(
			async () => await repo.GetByCountryAndLobsAsync(null!, ["property"], CancellationToken.None),
			Throws.InstanceOf<ArgumentException>());

		Assert.That(
			async () => await repo.GetByCountryAndLobsAsync("", ["property"], CancellationToken.None),
			Throws.InstanceOf<ArgumentException>());
	}

	[Test]
	public void GetByCountryAndLobsAsync_ThrowsArgumentNullException_WhenLobsIsNull()
	{
		// Arrange
		var repo = new GwpRepository([]);

		// Act & Assert
		Assert.That(
			async () => await repo.GetByCountryAndLobsAsync("ae", null!, CancellationToken.None),
			Throws.ArgumentNullException);
	}

	[Test]
	public void Constructor_ThrowsArgumentNullException_WhenRecordsIsNull()
	{
		// Act & Assert
		Assert.That(() => new GwpRepository(null!), Throws.ArgumentNullException);
	}
}