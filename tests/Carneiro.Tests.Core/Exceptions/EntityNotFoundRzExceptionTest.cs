using Carneiro.Core.Exceptions;

namespace Carneiro.Tests.Core.Exceptions;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class EntityNotFoundRzExceptionTest
{
    private Faker _faker;

    [SetUp]
    public void Setup() => _faker = new Faker();

    [Test]
    public void EntityNotFoundRzException_Ok()
    {
        var entity = _faker.Company.CompanyName();
        var exception = new EntityNotFoundRzException(entity);

        Assert.That(exception, Is.InstanceOf<RzException>());
        Assert.That(exception.Entity, Is.EqualTo(entity));
        Assert.That(exception.Id, Is.Null.Or.Empty);

        var value = _faker.Random.Int(min: 1);

        exception = new EntityNotFoundRzException(entity, value);
        Assert.That(exception.Entity, Is.EqualTo(entity));
        Assert.That(exception.Id, Is.EqualTo(value.ToString()));

        var anotherValue = _faker.Commerce.Department();
        exception = new EntityNotFoundRzException(entity, anotherValue);
        Assert.That(exception.Entity, Is.EqualTo(entity));
        Assert.That(exception.Id, Is.EqualTo(anotherValue));

    }

}