using System;

namespace Carneiro.Core.Extensions;

/// <summary>
/// <see cref="DateTime"/> Extensions.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Gets the human readable date difference.
    /// </summary>
    /// <param name="dateTime">The dateTime.</param>
    /// <returns></returns>
    public static string GetHumanReadableDateDifference(this DateTime dateTime)
    {
        // 1.
        // Get time span elapsed since the date.

        DateTime now = DateTime.UtcNow;
        TimeSpan s = now.Subtract(dateTime);

        // 2.
        // Get total number of days elapsed.
        var dayDiff = (int)s.TotalDays;

        // 3.
        // Get total number of seconds elapsed.
        var secDiff = (int)s.TotalSeconds;

        // 4.
        // Don't allow out of range values.
        if (dayDiff < 0)
            return null;

        switch (dayDiff)
        {
            // 5.
            // Handle same-day times.
            // A.
            // Less than one minute ago.
            case 0 when secDiff < 60:
                return "just now";
            // B.
            // Less than 2 minutes ago.
            case 0 when secDiff < 120:
                return "1 minute ago";
            // C.
            // Less than one hour ago.
            case 0 when secDiff < 3600:
                return $"{Math.Floor((double)secDiff / 60)} minutes ago";
            // D.
            // Less than 2 hours ago.
            case 0 when secDiff < 7200:
                return "1 hour ago";
            // E.
            // Less than one day ago.
            case 0 when secDiff < 86400:
                return $"{Math.Floor((double)secDiff / 3600)} hours ago";
            case 1:
                return "yesterday";
        }

        if (dayDiff < 7)
            return $"{dayDiff} days ago";

        if (dayDiff < 14)
            return "1 week ago";

        if (dayDiff < 31)
            return $"{Math.Ceiling((double)dayDiff / 7)} weeks ago";

        if (dayDiff < 60)
            return "1 month ago";

        if (dayDiff < 365)
            return $"{MonthDifference(now, dateTime)} months ago";

        if (dayDiff < 730)
            return "one year ago";

        var zeroTime = new DateTime(1, 1, 1);

        return $"{(zeroTime + s).Year - 1} years ago";
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> into a string format of <c>HH:mm:ss</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    /// <remarks>Returns <see cref="string.Empty"/> case <paramref name="dateTime"/> is null.</remarks>
    public static string ToTimeString(this DateTime? dateTime) => dateTime.HasValue ? dateTime.Value.ToTimeString() : string.Empty;

    /// <summary>
    /// Converts a <see cref="DateTime"/> into a string format of <c>HH:mm:ss</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static string ToTimeString(this DateTime dateTime) => dateTime.ToString("HH:mm:ss");

    /// <summary>
    /// Converts a <see cref="DateTime"/> into a string format of <c>HH:mm:ss</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    /// <remarks>Returns <see cref="string.Empty"/> case <paramref name="dateTime"/> is null.</remarks>
    public static string ToDateString(this DateTime? dateTime) => dateTime.HasValue ? dateTime.Value.ToDateString() : string.Empty;

    /// <summary>
    /// Converts a <see cref="DateTime"/> into a string format of <c>yyyy-MM-dd</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static string ToDateString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd");

    /// <summary>
    /// Converts a <see cref="DateTime"/> into a string format of <c>yyyy-MM-dd HH:mm:ss</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    /// <remarks>Returns <see cref="string.Empty"/> case <paramref name="dateTime"/> is null.</remarks>
    public static string ToDateTimeString(this DateTime? dateTime) => dateTime.HasValue ? dateTime.Value.ToDateTimeString() : string.Empty;

    /// <summary>
    /// Converts a <see cref="DateTime"/> into a string format of <c>yyyy-MM-dd HH:mm:ss</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static string ToDateTimeString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss");

    /// <summary>
    /// Converts a <see cref="DateTime" /> of <see cref="DateTimeKind.Utc" /> into a <see cref="DateTime"/> based in a <paramref name="timeZoneId"/>. Default is <c>GMT Standard Time</c>.
    /// </summary>
    /// <param name="dateTime">The time.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <returns></returns>
    /// <remarks>Returns <c>null</c> case <paramref name="dateTime"/> is null.</remarks>
    public static DateTime? ToTimeZoneTime(this DateTime? dateTime, string timeZoneId = "GMT Standard Time") => dateTime?.ToTimeZoneTime(timeZoneId);

    /// <summary>
    /// Converts a <see cref="DateTime" /> of <see cref="DateTimeKind.Utc" /> into a <see cref="DateTime"/> based in a <paramref name="timeZoneId"/>. Default is <c>GMT Standard Time</c>.
    /// </summary>
    /// <param name="dateTime">The time.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <returns></returns>
    public static DateTime ToTimeZoneTime(this DateTime dateTime, string timeZoneId = "GMT Standard Time")
    {
        var tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return dateTime.ToTimeZoneTime(tzi);
    }

    /// <summary>
    /// Converts a <see cref="DateTime" /> of <see cref="DateTimeKind.Utc" /> into a <see cref="DateTime"/> based in a <paramref name="timeZoneId"/>.
    /// </summary>
    /// <param name="dateTime">The time.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <returns></returns>
    public static DateTime ToTimeZoneTime(this DateTime dateTime, TimeZoneInfo timeZoneId) => TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneId);

    /// <summary>
    /// Converts a <see cref="string" /> into a <see cref="DateTime" />.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    /// <remarks>Case the <paramref name="str"/> is <c>null</c> or empty, it will return null. Case the parse can't be done, it will return <c>null</c>.</remarks>
    public static DateTime? ToDateTime(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return null;

        if (DateTime.TryParse(str, out DateTime dateTime))
        {
            return dateTime;
        }

        return null;
    }

    /// <summary>
    /// Converts a <see cref="string"/> into a <see cref="DateTime"/> with time <c>00:00:00</c>.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    /// <remarks>Returns <c>null</c> case <paramref name="str"/> is null.</remarks>
    public static DateTime? ToDateTimeZeroHours(this string str) => str.ToDateTime().ToDateTimeZeroHours();

    /// <summary>
    /// Converts a <see cref="string" /> into a <see cref="DateTime" /> with time <c>00:00:00</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    /// <remarks> Returns <c>null</c> case <paramref name="dateTime" /> is null. </remarks>
    public static DateTime? ToDateTimeZeroHours(this DateTime? dateTime)
    {
        if (!dateTime.HasValue)
            return null;

        return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 0, 0, 0);
    }

    /// <summary>
    /// Converts a <see cref="string"/> into a <see cref="DateTime"/> with time <c>23:59:59</c>.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    /// <remarks>Returns <c>null</c> case <paramref name="str"/> is null.</remarks>
    public static DateTime? ToDateTimeLastHours(this string str) => str.ToDateTime().ToDateTimeLastHours();

    /// <summary>
    /// Converts a <see cref="string"/> into a <see cref="DateTime"/> with time <c>23:59:59</c>.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    /// <remarks>Returns <c>null</c> case <paramref name="dateTime"/> is null.</remarks>
    public static DateTime? ToDateTimeLastHours(this DateTime? dateTime)
    {
        if (!dateTime.HasValue)
            return null;

        return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 23, 59, 59);
    }

    private static int MonthDifference(this DateTime lValue, DateTime rValue) => Math.Abs(lValue.Month - rValue.Month + 12 * (lValue.Year - rValue.Year));
}