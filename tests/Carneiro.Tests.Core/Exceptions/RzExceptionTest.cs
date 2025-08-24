using Carneiro.Core.Exceptions;

namespace Carneiro.Tests.Core.Exceptions;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class RzExceptionTest
{
    private Faker _faker;

    [SetUp]
    public void Setup() => _faker = new Faker();

    [Test]
    public void When_RzException_Ok()
    {
        var innerException = new ApplicationException(_faker.Hacker.Phrase());
        var message = _faker.Hacker.Phrase();

        RzException exception = Assert.Throws<RzException>(() => throw new RzException());
        Assert.That(exception.InnerException, Is.Null);
        Assert.That(exception.Message, Is.Not.Null.Or.Empty);

        exception = Assert.Throws<RzException>(() => throw new RzException(message));
        Assert.That(exception.InnerException, Is.Null);
        Assert.That(exception.Message, Is.EqualTo(message));

        exception = Assert.Throws<RzException>(() => throw new RzException(innerException));
        Assert.That(exception.InnerException, Is.EqualTo(innerException));
        Assert.That(exception.Message, Is.Not.Null.Or.Empty);

        exception = Assert.Throws<RzException>(() => throw new RzException(message, innerException));
        Assert.That(exception.InnerException, Is.EqualTo(innerException));
        Assert.That(exception.Message, Is.EqualTo(message));
    }
}