using CountryGwp.Infrastructure.Services;

namespace CountryGwp.Tests.Infrastructure;

[TestFixture]
public class CsvLoaderTests
{
	private string _tempFilePath = null!;

	[TearDown]
	public void CleanUp()
	{
		if (_tempFilePath != null && File.Exists(_tempFilePath))
			File.Delete(_tempFilePath);
	}

	[Test]
	public void Load_ThrowsArgumentException_WhenFilePathIsNullOrWhiteSpace()
	{
		Assert.That(() => CsvLoader.Load(null!), Throws.InstanceOf<ArgumentException>());
		Assert.That(() => CsvLoader.Load(""), Throws.InstanceOf<ArgumentException>());
		Assert.That(() => CsvLoader.Load("   "), Throws.InstanceOf<ArgumentException>());
	}

	[Test]
	public void Load_ThrowsFileNotFoundException_WhenFileDoesNotExist()
	{
		Assert.That(() => CsvLoader.Load("not_existing_file.csv"), Throws.InstanceOf<FileNotFoundException>());
	}

	[Test]
	public void Load_ReturnsEmpty_WhenFileHasNoDataRows()
	{
		// Arrange
		_tempFilePath = Path.GetTempFileName();
		File.WriteAllText(_tempFilePath, "country,variableId,lineOfBusiness,Y2010,Y2011");

		// Act
		var result = CsvLoader.Load(_tempFilePath);

		// Assert
		Assert.That(result, Is.Empty);
	}

	[Test]
	public void Load_ParsesValidCsv_Correctly()
	{
		// Arrange
		_tempFilePath = Path.GetTempFileName();
		var text = new[]
		{
			"country,variableId,lineOfBusiness,Y2010,Y2011",
			"ae,gwp,property,100,200",
			"ae,gwp,transport,300,",
			"us,gwp,property,400,500"
		};
		var csv = string.Join(Environment.NewLine, text);
		File.WriteAllText(_tempFilePath, csv);

		// Act
		var result = CsvLoader.Load(_tempFilePath).ToList();

		// Assert
		Assert.That(result.Count, Is.EqualTo(3));
		var first = result[0];
		Assert.That(first.Country.Value, Is.EqualTo("ae"));
		Assert.That(first.LineOfBusiness.Value, Is.EqualTo("property"));
		Assert.That(first.VariableId.Value, Is.EqualTo("gwp"));
		Assert.That(first.YearlyGwp.Values["Y2010"], Is.EqualTo(100m));
		Assert.That(first.YearlyGwp.Values["Y2011"], Is.EqualTo(200m));
		var second = result[1];
		Assert.That(second.YearlyGwp.Values["Y2011"], Is.Null);
	}
}