namespace Carneiro.Tests.Core.Extensions;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class DecimalExtensionsTests
{
    [Test]
    [TestCase(0, "0.00")]
    [TestCase(1, "1.00")]
    [TestCase(1.153, "1.15")]
    [TestCase(1.153439439439, "1.15")]
    [TestCase(1.1535, "1.15")]
    [TestCase(1.1539, "1.15")]
    [TestCase(1.9912, "1.99")]
    public void ToQuantityFormat_Actual_IsExpected(decimal actual, string expected)
    {
        var s = actual.ToQuantityFormat();

        Assert.That(s.Replace(",", "."), Is.EqualTo(expected));
    }
}