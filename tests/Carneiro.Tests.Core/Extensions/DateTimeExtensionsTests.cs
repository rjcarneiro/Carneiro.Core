namespace Carneiro.Tests.Core.Extensions;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class DateTimeExtensionsTests
{
    private Dictionary<DateTime, string> _items;

    [SetUp]
    public void Setup() => _items = new Dictionary<DateTime, string>
    {
        {DateTime.UtcNow, "just now"},
        {DateTime.UtcNow.AddMinutes(-1), "1 minute ago"},
        {DateTime.UtcNow.AddMinutes(-2), "2 minutes ago"},
        {DateTime.UtcNow.AddMinutes(-5), "5 minutes ago"},
        {DateTime.UtcNow.AddHours(-1), "1 hour ago"},
        {DateTime.UtcNow.AddHours(-2), "2 hours ago"},
        {DateTime.UtcNow.AddHours(-3), "3 hours ago"},
        {DateTime.UtcNow.AddDays(-1), "yesterday"},
        {DateTime.UtcNow.AddDays(-2), "2 days ago"},
        {DateTime.UtcNow.AddDays(-3), "3 days ago"},
        {DateTime.UtcNow.AddDays(-4), "4 days ago"},
        {DateTime.UtcNow.AddDays(-5), "5 days ago"},
        {DateTime.UtcNow.AddDays(-6), "6 days ago"},
        {DateTime.UtcNow.AddDays(-7), "1 week ago"},
        {DateTime.UtcNow.AddDays(-8), "1 week ago"},
        {DateTime.UtcNow.AddDays(-9), "1 week ago"},
        {DateTime.UtcNow.AddDays(-10), "1 week ago"},
        {DateTime.UtcNow.AddDays(-11), "1 week ago"},
        {DateTime.UtcNow.AddDays(-12), "1 week ago"},
        {DateTime.UtcNow.AddDays(-13), "1 week ago"},
        {DateTime.UtcNow.AddDays(-14), "2 weeks ago"},
        {DateTime.UtcNow.AddDays(-15), "3 weeks ago"},
        {DateTime.UtcNow.AddDays(-16), "3 weeks ago"},
        {DateTime.UtcNow.AddDays(-17), "3 weeks ago"},
        {DateTime.UtcNow.AddDays(-22), "4 weeks ago"},
        {DateTime.UtcNow.AddDays(-30), "5 weeks ago"},
        {DateTime.UtcNow.AddDays(-31), "1 month ago"},
        {DateTime.UtcNow.AddMonths(-2), "2 months ago"},
        {DateTime.UtcNow.AddMonths(-3), "3 months ago"},
        {DateTime.UtcNow.AddMonths(-4), "4 months ago"},
        {DateTime.UtcNow.AddMonths(-5), "5 months ago"},
        {DateTime.UtcNow.AddMonths(-6), "6 months ago"},
        {DateTime.UtcNow.AddMonths(-7), "7 months ago"},
        {DateTime.UtcNow.AddMonths(-8), "8 months ago"},
        {DateTime.UtcNow.AddMonths(-9), "9 months ago"},
        {DateTime.UtcNow.AddMonths(-10), "10 months ago"},
        {DateTime.UtcNow.AddMonths(-11), "11 months ago"},
        {DateTime.UtcNow.AddMonths(-12), "one year ago"},
        {DateTime.UtcNow.AddYears(-1), "one year ago"},
        {DateTime.UtcNow.AddYears(-2), "2 years ago"},
        {DateTime.UtcNow.AddYears(-3), "3 years ago"}
    };

    [Test]
    public void When_GetHumanReadableDateDifference_Match()
    {
        foreach ((DateTime key, var value) in _items)
        {
            Assert.That(key.GetHumanReadableDateDifference(), Is.EqualTo(value));
        }
    }
}