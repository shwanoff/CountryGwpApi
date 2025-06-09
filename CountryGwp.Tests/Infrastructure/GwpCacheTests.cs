using CountryGwp.Infrastructure.Caching;

namespace CountryGwp.Tests.Infrastructure;

[TestFixture]
public class GwpCacheTests
{
	[Test]
	public void TryGet_ReturnsFalse_WhenKeyDoesNotExist()
	{
		// Arrange
		var cache = new GwpCache();

		// Act
		var found = cache.TryGet("notFound", out var value);

		// Assert
		Assert.That(found, Is.False);
		Assert.That(value, Is.EqualTo(0m));
	}

	[Test]
	public void Set_And_TryGet_ReturnsTrue_WhenKeyExists()
	{
		// Arrange
		var cache = new GwpCache();
		var key = "country|lob|2008|2015";
		cache.Set(key, 123.45m);

		// Act
		var found = cache.TryGet(key, out var value);

		// Assert
		Assert.That(found, Is.True);
		Assert.That(value, Is.EqualTo(123.45m));
	}

	[Test]
	public void Set_OverridesValue_ForSameKey()
	{
		// Arrange
		var cache = new GwpCache();
		var key = "country|lob|2008|2015";
		cache.Set(key, 1.1m);

		// Act
		cache.Set(key, 2.2m);
		var found = cache.TryGet(key, out var value);

		// Assert
		Assert.That(found, Is.True);
		Assert.That(value, Is.EqualTo(2.2m));
	}

	[Test]
	public void Clear_RemovesAllEntries()
	{
		// Arrange
		var cache = new GwpCache();
		cache.Set("a", 1m);
		cache.Set("b", 2m);

		// Act
		cache.Clear();

		// Assert
		Assert.That(cache.TryGet("a", out _), Is.False);
		Assert.That(cache.TryGet("b", out _), Is.False);
	}
}