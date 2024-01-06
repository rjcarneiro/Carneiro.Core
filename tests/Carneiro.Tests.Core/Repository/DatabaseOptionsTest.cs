using Carneiro.Core.Repository.Options;

namespace Carneiro.Tests.Core.Repository;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class DatabaseOptionsTest
{
    [Test]
    public void When_DbSettings_Default()
    {
        var options = new DatabaseOptions();

        Assert.That(options.EnableDetailedErrors, Is.False);
        Assert.That(options.EnableDetailedErrors, Is.False);
        Assert.That(options.Timeout, Is.EqualTo(60));

        Assert.That(options.Failure, Is.Not.Null);
        Assert.That(options.Failure.Retries, Is.EqualTo(3));
        Assert.That(options.Failure.Seconds, Is.EqualTo(15));
    }
}