using Carneiro.Core.Web;

namespace Carneiro.Tests.Core.Web;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class ContentTypeHelpersTests
{
    [Test]
    public void AllImageContentTypes_HasValues() => Assert.That(ContentTypeHelpers.AllImageContentTypes, Has.Length.GreaterThan(0));

    [Test]
    public void AvailableImageContentTypes_HasValues() => Assert.That(ContentTypeHelpers.AvailableImageContentTypes, Has.Length.GreaterThan(0));
}