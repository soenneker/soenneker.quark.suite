using System;
using System.Globalization;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class QuarkDateTimeFormatterTests
{
    private static readonly CultureInfo EnUs = CultureInfo.GetCultureInfo("en-US");
    private readonly QuarkDateTimeFormatter _formatter = new();

    [Test]
    public void Relative_formats_just_now()
    {
        var now = Utc(2026, 5, 27, 15, 0);

        _formatter.FormatRelative(now.AddSeconds(-5), now, Options()).Should().Be("just now");
    }

    [Test]
    public void Relative_formats_seconds_ago()
    {
        var now = Utc(2026, 5, 27, 15, 0);

        _formatter.FormatRelative(now.AddSeconds(-30), now, Options()).Should().Be("30 seconds ago");
    }

    [Test]
    public void Relative_formats_minutes_ago()
    {
        var now = Utc(2026, 5, 27, 15, 0);

        _formatter.FormatRelative(now.AddMinutes(-5), now, Options()).Should().Be("5 minutes ago");
    }

    [Test]
    public void Relative_formats_hours_ago()
    {
        var now = Utc(2026, 5, 27, 15, 0);

        _formatter.FormatRelative(now.AddHours(-2), now, Options()).Should().Be("2 hours ago");
    }

    [Test]
    public void Relative_formats_yesterday()
    {
        var now = Utc(2026, 5, 27, 15, 0);

        _formatter.FormatRelative(Utc(2026, 5, 26, 8, 0), now, Options()).Should().Be("yesterday");
    }

    [Test]
    public void Relative_formats_future_values()
    {
        var now = Utc(2026, 1, 15, 12, 0);

        _formatter.FormatRelative(now.AddMinutes(4), now, Options()).Should().Be("in 4 minutes");
        _formatter.FormatRelative(now.AddDays(2), now, Options()).Should().Be("in 2 days");
        _formatter.FormatRelative(Utc(2026, 4, 15, 12, 0), now, Options()).Should().Be("in 3 months");
        _formatter.FormatRelative(Utc(2027, 1, 15, 12, 0), now, Options()).Should().Be("in 1 year");
    }

    [Test]
    public void Until_formats_remaining_and_expired_values()
    {
        var now = Utc(2026, 5, 27, 15, 0);

        _formatter.FormatUntil(now.AddSeconds(4), now, Options()).Should().Be("4 seconds");
        _formatter.FormatUntil(now.AddMinutes(3), now, Options()).Should().Be("3 minutes");
        _formatter.FormatUntil(now.AddHours(2), now, Options()).Should().Be("2 hours");
        _formatter.FormatUntil(now.AddDays(5), now, Options()).Should().Be("5 days");
        _formatter.FormatUntil(now.AddSeconds(-1), now, Options()).Should().Be("expired");
        _formatter.FormatUntil(now.AddSeconds(-1), now, Options(expiredText: "Closed")).Should().Be("Closed");
    }

    [Test]
    public void Calendar_formats_today_tomorrow_and_yesterday()
    {
        var now = Utc(2026, 5, 27, 15, 42);

        _formatter.FormatCalendar(now, now, Options()).Should().Be("Just now");
        _formatter.FormatCalendar(Utc(2026, 5, 27, 18, 15), now, Options()).Should().Be("Today at 6:15 PM");
        _formatter.FormatCalendar(Utc(2026, 5, 28, 9, 0), now, Options()).Should().Be("Tomorrow at 9:00 AM");
        _formatter.FormatCalendar(Utc(2026, 5, 26, 18, 15), now, Options()).Should().Be("Yesterday at 6:15 PM");
    }

    [Test]
    public void Calendar_formats_last_and_next_weekdays()
    {
        var now = Utc(2026, 5, 27, 15, 42);

        _formatter.FormatCalendar(Utc(2026, 5, 22, 14, 0), now, Options()).Should().Be("Last Friday at 2:00 PM");
        _formatter.FormatCalendar(Utc(2026, 6, 1, 10, 30), now, Options()).Should().Be("Next Monday at 10:30 AM");
    }

    [Test]
    public void Calendar_handles_month_and_year_boundaries()
    {
        _formatter.FormatCalendar(Utc(2026, 2, 1, 9, 0), Utc(2026, 1, 31, 12, 0), Options()).Should().Be("Tomorrow at 9:00 AM");
        _formatter.FormatCalendar(Utc(2027, 1, 1, 9, 0), Utc(2026, 12, 31, 12, 0), Options()).Should().Be("Tomorrow at 9:00 AM");
    }

    [Test]
    public void Calendar_formats_older_dates()
    {
        var now = Utc(2026, 5, 27, 15, 42);

        _formatter.FormatCalendar(Utc(2026, 1, 12, 16, 0), now, Options()).Should().Be("Jan 12 at 4:00 PM");
        _formatter.FormatCalendar(Utc(2025, 1, 12, 16, 0), now, Options()).Should().Be("Jan 12, 2025 at 4:00 PM");
    }

    [Test]
    public void Invalid_timezone_falls_back_to_browser_timezone()
    {
        var value = Utc(2026, 1, 1, 0, 0);
        var options = Options(timeZone: "Not/A_TimeZone", browserTimeZone: "UTC");

        _formatter.FormatDateTimeAttribute(value, options).Should().EndWith("+00:00");
    }

    [Test]
    public void Invalid_culture_name_falls_back_to_current_culture()
    {
        var originalCulture = CultureInfo.CurrentCulture;

        try
        {
            CultureInfo.CurrentCulture = EnUs;

            _formatter.Format(Utc(2026, 1, 1, 0, 0), new QuarkDateTimeFormatOptions
            {
                TimeZone = "UTC",
                CultureName = "invalid-culture",
                Format = "MMMM d, yyyy"
            }).Should().Be("January 1, 2026");
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    private static QuarkDateTimeFormatOptions Options(string? timeZone = "UTC", string? browserTimeZone = null, string? expiredText = null)
    {
        return new QuarkDateTimeFormatOptions
        {
            TimeZone = timeZone,
            BrowserTimeZone = browserTimeZone,
            Culture = EnUs,
            ExpiredText = expiredText
        };
    }

    private static DateTimeOffset Utc(int year, int month, int day, int hour, int minute)
    {
        return new DateTimeOffset(year, month, day, hour, minute, 0, TimeSpan.Zero);
    }
}
