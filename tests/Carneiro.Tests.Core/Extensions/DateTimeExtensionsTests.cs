namespace Carneiro.Tests.Core.Extensions;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class DateTimeExtensionsTests
{
    [Test]
    [TestCaseSource(nameof(s_dateCases))]
    public void When_GetHumanReadableDateDifference_Match(DateTime key, string value) => Assert.That(key.ToHumanReadableDateDifference(), Is.EqualTo(value));

    private static readonly object[] s_dateCases =
    [
        new object[] { DateTime.UtcNow, "just now" },
        new object[] { DateTime.UtcNow.AddMinutes(-1), "1 minute ago" },
        new object[] { DateTime.UtcNow.AddMinutes(-2), "2 minutes ago" },
        new object[] { DateTime.UtcNow.AddMinutes(-5), "5 minutes ago" },
        new object[] { DateTime.UtcNow.AddHours(-1), "1 hour ago" },
        new object[] { DateTime.UtcNow.AddHours(-2), "2 hours ago" },
        new object[] { DateTime.UtcNow.AddHours(-3), "3 hours ago" },
        new object[] { DateTime.UtcNow.AddDays(-1), "yesterday" },
        new object[] { DateTime.UtcNow.AddDays(-2), "2 days ago" },
        new object[] { DateTime.UtcNow.AddDays(-3), "3 days ago" },
        new object[] { DateTime.UtcNow.AddDays(-4), "4 days ago" },
        new object[] { DateTime.UtcNow.AddDays(-5), "5 days ago" },
        new object[] { DateTime.UtcNow.AddDays(-6), "6 days ago" },
        new object[] { DateTime.UtcNow.AddDays(-7), "1 week ago" },
        new object[] { DateTime.UtcNow.AddDays(-8), "1 week ago" },
        new object[] { DateTime.UtcNow.AddDays(-9), "1 week ago" },
        new object[] { DateTime.UtcNow.AddDays(-10), "1 week ago" },
        new object[] { DateTime.UtcNow.AddDays(-11), "1 week ago" },
        new object[] { DateTime.UtcNow.AddDays(-12), "1 week ago" },
        new object[] { DateTime.UtcNow.AddDays(-13), "1 week ago" },
        new object[] { DateTime.UtcNow.AddDays(-14), "2 weeks ago" },
        new object[] { DateTime.UtcNow.AddDays(-15), "3 weeks ago" },
        new object[] { DateTime.UtcNow.AddDays(-16), "3 weeks ago" },
        new object[] { DateTime.UtcNow.AddDays(-17), "3 weeks ago" },
        new object[] { DateTime.UtcNow.AddDays(-22), "4 weeks ago" },
        new object[] { DateTime.UtcNow.AddDays(-30), "5 weeks ago" },
        new object[] { DateTime.UtcNow.AddDays(-31), "1 month ago" },
        new object[] { DateTime.UtcNow.AddMonths(-2).AddDays(-1), "2 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-3), "3 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-4), "4 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-5), "5 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-6), "6 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-7), "7 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-8), "8 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-9), "9 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-10), "10 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-11), "11 months ago" },
        new object[] { DateTime.UtcNow.AddMonths(-12), "1 year ago" },
        new object[] { DateTime.UtcNow.AddYears(-1), "1 year ago" },
        new object[] { DateTime.UtcNow.AddYears(-2), "2 years ago" },
        new object[] { DateTime.UtcNow.AddYears(-3), "3 years ago" }
    ];
};