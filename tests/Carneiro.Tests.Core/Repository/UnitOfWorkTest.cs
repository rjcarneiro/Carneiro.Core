using Carneiro.Core.Repository;
using Carneiro.Core.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Carneiro.Tests.Core.Repository;

[TestFixture]
public class UnitOfWorkTest
{
    [Test]
    public void When_UnitOfWork_Ok()
    {
        var unitOfWork = new UnitOfWork<DbContext>(new Mock<DbContext>().Object, new Mock<ILogger<UnitOfWork<DbContext>>>().Object);
        Assert.That(unitOfWork, Is.InstanceOf<IUnitOfWork>());
        Assert.That(unitOfWork, Is.InstanceOf<IDisposable>());
    }
}