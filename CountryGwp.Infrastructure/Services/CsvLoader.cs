using CountryGwp.Domain.Entities;
using CountryGwp.Domain.ValueObjects;
using System.Globalization;

namespace CountryGwp.Infrastructure.Services;

/// <summary>
/// Provides functionality to load gross written premium (GWP) records from a CSV file.
/// Parses the CSV and maps each row to a <see cref="GwpRecord"/> instance, including country, line of business, variable, and yearly GWP values.
/// </summary>
public static class CsvLoader
{
    /// <summary>
    /// Loads GWP records from the specified CSV file path.
    /// </summary>
    /// <param name="filePath">The path to the CSV file containing GWP data.</param>
    /// <returns>
    /// An enumerable collection of <see cref="GwpRecord"/> objects parsed from the CSV file.
    /// </returns>
    public static IEnumerable<GwpRecord> Load(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The specified file does not exist: {filePath}");

		var records = new List<GwpRecord>();
        var lines = File.ReadAllLines(filePath);
        if (lines.Length < 2)
            return records;

        var header = lines[0].Split(',');
        int idxCountry = Array.IndexOf(header, "country");
        int idxVariableId = Array.IndexOf(header, "variableId");
        int idxLineOfBusiness = Array.IndexOf(header, "lineOfBusiness");

        var yearColumns = header
            .Select((h, i) => new { h, i })
            .Where(x => x.h.StartsWith("Y"))
            .ToList();

        for (int i = 1; i < lines.Length; i++)
        {
            var row = lines[i].Split(',');

            if (row.Length < 4)
                continue;

            var country = new CountryCode(row[idxCountry].Trim());
            var lob = new LineOfBusiness(row[idxLineOfBusiness].Trim());
            var variable = new Variable(row[idxVariableId].Trim());

            var values = new Dictionary<string, decimal?>();
            foreach (var yc in yearColumns)
            {
                var valueStr = yc.i < row.Length ? row[yc.i].Trim() : null;
                if (!string.IsNullOrEmpty(valueStr) && decimal.TryParse(valueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    values[yc.h] = value;
                }
                else
                {
                    values[yc.h] = null;
                }
            }

            var yearlyGwp = new YearlyGwp(values);

            records.Add(new GwpRecord
            {
                Country = country,
                LineOfBusiness = lob,
                VariableId = variable,
                YearlyGwp = yearlyGwp
            });
        }

        return records;
    }
}
