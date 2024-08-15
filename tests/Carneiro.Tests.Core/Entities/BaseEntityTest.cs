using Bogus;
using Carneiro.Core.Entities;
using Carneiro.Core.Entities.Abstractions;

namespace Carneiro.Tests.Core.Entities;

[TestFixture]
public class BaseEntityTest
{
    private Faker _faker;

    [SetUp]
    public void Setup() => _faker = new Faker();

    [Test]
    public void When_NewInstance_Ok()
    {
        IEntity instance = new MyEntity
        {
            Id = _faker.Random.Int(min: 1),
            IsActive = _faker.PickRandom(true, false),
            CreateDate = _faker.Date.Recent(),
            IsDeleted = _faker.PickRandom(true, false),
            DeleteDate = _faker.Date.Recent(),
            UpdateDate = _faker.Date.Recent()
        };
        Assert.That(instance, Is.InstanceOf<AuditableEntity>());
        Assert.That(instance, Is.InstanceOf<IAuditableEntity>());

        Assert.That(instance.Id, Is.GreaterThan(0));
        Assert.That(instance.CreateDate, Is.GreaterThan(DateTime.MinValue));
        Assert.That(instance.DeleteDate, Is.GreaterThan(DateTime.MinValue));
        Assert.That(instance.UpdateDate, Is.GreaterThan(DateTime.MinValue));

        var id = _faker.Random.Int(min: 1);
        var isActive = _faker.PickRandom(true, false);
        var isDeleted = _faker.PickRandom(true, false);
        DateTime createDate = _faker.Date.Past();
        DateTime? updateDate = _faker.PickRandom<DateTime?>(null, DateTime.UtcNow);
        DateTime? deleteDate = _faker.PickRandom<DateTime?>(null, DateTime.UtcNow);

        instance.Id = id;
        instance.IsActive = isActive;
        instance.IsDeleted = isDeleted;
        instance.CreateDate = createDate;
        instance.UpdateDate = updateDate;
        instance.DeleteDate = deleteDate;

        Assert.That(instance.Id, Is.EqualTo(id));
        Assert.That(instance.IsActive, Is.EqualTo(isActive));
        Assert.That(instance.IsDeleted, Is.EqualTo(isDeleted));
        Assert.That(instance.CreateDate, Is.EqualTo(createDate));
        Assert.That(instance.UpdateDate, Is.EqualTo(updateDate));
        Assert.That(instance.DeleteDate, Is.EqualTo(deleteDate));
    }
}