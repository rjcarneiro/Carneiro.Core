using Carneiro.Core.IpChecker;

namespace Carneiro.Tests.Core;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class IpAddressCheckerCacheItemExtensionsTests
{
    private readonly Faker _faker = new();

    [Test]
    public void IsCacheExpired_True()
    {
        // Arrange
        var currentDate = new DateTime(2020, 1, 1);

        var options = new IpAddressCheckerOptions
        {
            PersistenceCacheTimeout = TimeSpan.FromHours(12),
            UsePersistenceCache = true,
        };

        var item = new IpAddressCheckerCacheItem(_faker.Internet.Ip(), currentDate);

        // Act
        var isCacheExpired = item.IsCacheExpired(options);

        // Assert
        Assert.That(isCacheExpired, Is.True);
    }

    [Test]
    public void IsCacheExpired_NullPersistenceCacheTimeout_True()
    {
        // Arrange
        var currentDate = new DateTime(2020, 1, 1);

        var options = new IpAddressCheckerOptions
        {
            PersistenceCacheTimeout = null,
            UsePersistenceCache = true,
        };

        var item = new IpAddressCheckerCacheItem(_faker.Internet.Ip(), currentDate);

        // Act
        var isCacheExpired = item.IsCacheExpired(options);

        // Assert
        Assert.That(isCacheExpired, Is.True);
    }

    [Test]
    public void IsCacheExpired_False()
    {
        // Arrange
        var currentDate = DateTime.UtcNow;

        var options = new IpAddressCheckerOptions
        {
            PersistenceCacheTimeout = TimeSpan.FromHours(12),
            UsePersistenceCache = true,
        };

        var item = new IpAddressCheckerCacheItem(_faker.Internet.Ip(), currentDate);

        // Act
        var isCacheExpired = item.IsCacheExpired(options);

        // Assert
        Assert.That(isCacheExpired, Is.False);
    }
}