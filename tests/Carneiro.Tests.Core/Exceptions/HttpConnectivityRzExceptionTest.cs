using Carneiro.Core.Exceptions;

namespace Carneiro.Tests.Core.Exceptions;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class HttpConnectivityRzExceptionTest
{
    [Test]
    public void When_HttpConnectivityRzException_Ok()
    {
        var innerException = new ApplicationException();

        HttpConnectivityRzException exception = Assert.Throws<HttpConnectivityRzException>(() => throw new HttpConnectivityRzException(innerException));
        Assert.That(exception.InnerException, Is.EqualTo(innerException));

        Assert.That(exception, Is.InstanceOf<RzException>());

        var randomMessage = "Hello friends";

        exception = Assert.Throws<HttpConnectivityRzException>(() => throw new HttpConnectivityRzException(randomMessage));

        Assert.That(exception.InnerException, Is.Null);
        Assert.That(exception.Message, Is.EqualTo(randomMessage));

        exception = Assert.Throws<HttpConnectivityRzException>(() => throw new HttpConnectivityRzException(randomMessage, innerException));

        Assert.That(exception.InnerException, Is.EqualTo(innerException));
        Assert.That(exception.Message, Is.EqualTo(randomMessage));
    }
}