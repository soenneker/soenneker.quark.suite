using System;
using System.Globalization;

namespace Soenneker.Quark;

/// <inheritdoc cref="IQuarkDateTimeFormatter"/>
public sealed class QuarkDateTimeFormatter : IQuarkDateTimeFormatter
{
    private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
    private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan OneHour = TimeSpan.FromHours(1);
    private static readonly TimeSpan OneDay = TimeSpan.FromDays(1);


    public DateTimeOffset Convert(DateTimeOffset value, QuarkDateTimeFormatOptions options)
    {
        var timeZone = QuarkDateTimeZoneResolver.Resolve(options.TimeZone, options.BrowserTimeZone);
        return TimeZoneInfo.ConvertTime(value, timeZone);
    }


    public string Format(DateTimeOffset value, QuarkDateTimeFormatOptions options)
    {
        var culture = ResolveCulture(options);
        var local = Convert(value, options);
        var format = string.IsNullOrWhiteSpace(options.Format) ? "f" : options.Format;

        return local.ToString(format, culture);
    }


    public string FormatRelative(DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options)
    {
        var timeZone = QuarkDateTimeZoneResolver.Resolve(options.TimeZone, options.BrowserTimeZone);
        var localValue = TimeZoneInfo.ConvertTime(value, timeZone);
        var localNow = TimeZoneInfo.ConvertTime(now, timeZone);
        var delta = localValue - localNow;
        var future = delta > TimeSpan.Zero;
        var absolute = delta.Duration();

        if (absolute < TimeSpan.FromSeconds(10))
            return IsShortRelativeFormat(options.DateRelativeFormatStyle) ? "now" : "just now";

        if (absolute < OneMinute)
            return FormatRelativeUnit((int)Math.Max(1, Math.Floor(absolute.TotalSeconds)), "second", "sec", future, options.DateRelativeFormatStyle);

        if (absolute < OneHour)
            return FormatRelativeUnit((int)Math.Max(1, Math.Floor(absolute.TotalMinutes)), "minute", "min", future, options.DateRelativeFormatStyle);

        var valueDate = DateOnly.FromDateTime(localValue.DateTime);
        var nowDate = DateOnly.FromDateTime(localNow.DateTime);
        var dayDiff = valueDate.DayNumber - nowDate.DayNumber;

        if (absolute < TimeSpan.FromHours(24) && Math.Abs(dayDiff) <= 1)
            return FormatRelativeUnit((int)Math.Max(1, Math.Floor(absolute.TotalHours)), "hour", "hr", future, options.DateRelativeFormatStyle);

        if (dayDiff == -1)
            return IsShortRelativeFormat(options.DateRelativeFormatStyle) ? "1 day" : "yesterday";

        if (dayDiff == 1)
            return IsShortRelativeFormat(options.DateRelativeFormatStyle) ? "1 day" : "tomorrow";

        var absoluteDays = Math.Abs(dayDiff);
        if (absoluteDays < 30)
            return FormatRelativeUnit(absoluteDays, "day", "day", future, options.DateRelativeFormatStyle, "days");

        var months = GetWholeMonthDifference(localValue, localNow);
        var absoluteMonths = Math.Abs(months);

        if (absoluteMonths < 12)
            return FormatRelativeUnit(Math.Max(1, absoluteMonths), "month", "mo", future, options.DateRelativeFormatStyle);

        var years = Math.Max(1, absoluteMonths / 12);
        return FormatRelativeUnit(years, "year", "yr", future, options.DateRelativeFormatStyle);
    }


    public string FormatUntil(DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options)
    {
        var timeZone = QuarkDateTimeZoneResolver.Resolve(options.TimeZone, options.BrowserTimeZone);
        var localValue = TimeZoneInfo.ConvertTime(value, timeZone);
        var localNow = TimeZoneInfo.ConvertTime(now, timeZone);
        var remaining = localValue - localNow;

        if (remaining <= TimeSpan.Zero)
            return string.IsNullOrWhiteSpace(options.ExpiredText) ? "expired" : options.ExpiredText!;

        if (remaining < OneSecond)
            return "now";

        if (remaining < OneMinute)
            return FormatUnit((int)Math.Ceiling(remaining.TotalSeconds), "second");

        if (remaining < OneHour)
            return FormatUnit((int)Math.Ceiling(remaining.TotalMinutes), "minute");

        if (remaining < OneDay)
            return FormatUnit((int)Math.Ceiling(remaining.TotalHours), "hour");

        return FormatUnit((int)Math.Ceiling(remaining.TotalDays), "day");
    }


    public string FormatCalendar(DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options)
    {
        var culture = ResolveCulture(options);
        var timeZone = QuarkDateTimeZoneResolver.Resolve(options.TimeZone, options.BrowserTimeZone);
        var localValue = TimeZoneInfo.ConvertTime(value, timeZone);
        var localNow = TimeZoneInfo.ConvertTime(now, timeZone);
        var absolute = (localValue - localNow).Duration();

        if (absolute < TimeSpan.FromSeconds(10))
            return "Just now";

        var valueDate = DateOnly.FromDateTime(localValue.DateTime);
        var nowDate = DateOnly.FromDateTime(localNow.DateTime);
        var dayDiff = valueDate.DayNumber - nowDate.DayNumber;
        if (dayDiff == 0)
            return string.Create(culture, $"Today at {localValue:t}");

        if (dayDiff == 1)
            return string.Create(culture, $"Tomorrow at {localValue:t}");

        if (dayDiff == -1)
            return string.Create(culture, $"Yesterday at {localValue:t}");

        if (dayDiff is >= 2 and <= 6)
            return string.Create(culture, $"Next {culture.DateTimeFormat.GetDayName(localValue.DayOfWeek)} at {localValue:t}");

        if (dayDiff is <= -2 and >= -6)
            return string.Create(culture, $"Last {culture.DateTimeFormat.GetDayName(localValue.DayOfWeek)} at {localValue:t}");

        return localValue.Year == localNow.Year
            ? string.Create(culture, $"{localValue:MMM d} at {localValue:t}")
            : string.Create(culture, $"{localValue:MMM d, yyyy} at {localValue:t}");
    }


    public string FormatTitle(DateTimeOffset value, QuarkDateTimeFormatOptions options)
    {
        var culture = ResolveCulture(options);
        var local = Convert(value, options);
        return local.ToString("F", culture);
    }


    public string FormatDateTimeAttribute(DateTimeOffset value, QuarkDateTimeFormatOptions options)
    {
        return Convert(value, options).ToString("O", CultureInfo.InvariantCulture);
    }


    public TimeSpan? GetNextUpdateInterval(QuarkDateTimeUpdateKind kind, DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options)
    {
        if (kind == QuarkDateTimeUpdateKind.Text)
            return null;

        var timeZone = QuarkDateTimeZoneResolver.Resolve(options.TimeZone, options.BrowserTimeZone);
        var localNow = TimeZoneInfo.ConvertTime(now, timeZone);

        return kind switch
        {
            QuarkDateTimeUpdateKind.Text => null,
            QuarkDateTimeUpdateKind.Now => GetNowInterval(localNow, options.Format),
            QuarkDateTimeUpdateKind.Relative => GetRelativeInterval(TimeZoneInfo.ConvertTime(value, timeZone), localNow),
            QuarkDateTimeUpdateKind.Until => GetUntilInterval(TimeZoneInfo.ConvertTime(value, timeZone), localNow),
            QuarkDateTimeUpdateKind.Calendar => GetCalendarInterval(TimeZoneInfo.ConvertTime(value, timeZone), localNow),
            _ => null
        };
    }

    private static CultureInfo ResolveCulture(QuarkDateTimeFormatOptions options)
    {
        if (options.Culture is not null)
            return options.Culture;

        if (!string.IsNullOrWhiteSpace(options.CultureName))
        {
            try
            {
                return CultureInfo.GetCultureInfo(options.CultureName);
            }
            catch (CultureNotFoundException)
            {
                return CultureInfo.CurrentCulture;
            }
        }

        return CultureInfo.CurrentCulture;
    }

    private static string FormatRelativeUnit(int value, string unit, string shortUnit, bool future, DateRelativeFormatStyle style, string? shortPluralUnit = null)
    {
        if (IsShortRelativeFormat(style))
            return FormatShortUnit(value, shortUnit, shortPluralUnit);

        string plural = value == 1 ? string.Empty : "s";
        return future
            ? string.Create(CultureInfo.InvariantCulture, $"in {value} {unit}{plural}")
            : string.Create(CultureInfo.InvariantCulture, $"{value} {unit}{plural} ago");
    }

    private static bool IsShortRelativeFormat(DateRelativeFormatStyle style)
    {
        return ReferenceEquals(style, DateRelativeFormatStyle.Short);
    }

    private static string FormatShortUnit(int value, string unit, string? pluralUnit = null)
    {
        var resolvedUnit = value == 1 ? unit : pluralUnit ?? unit;
        return string.Create(CultureInfo.InvariantCulture, $"{value} {resolvedUnit}");
    }

    private static string FormatUnit(int value, string unit)
    {
        return value == 1 ? $"1 {unit}" : string.Create(CultureInfo.InvariantCulture, $"{value} {unit}s");
    }

    private static int GetWholeMonthDifference(DateTimeOffset value, DateTimeOffset now)
    {
        var months = (value.Year - now.Year) * 12 + value.Month - now.Month;

        if (months > 0 && value.Day < now.Day)
            months--;
        else if (months < 0 && value.Day > now.Day)
            months++;

        return months;
    }

    private static TimeSpan GetNowInterval(DateTimeOffset now, string? format)
    {
        if (FormatHasSecondPrecision(format))
            return OneSecond;

        if (FormatHasMinutePrecision(format))
            return UntilNextMinute(now);

        return UntilNextDay(now);
    }

    private static TimeSpan GetRelativeInterval(DateTimeOffset value, DateTimeOffset now)
    {
        var delta = value - now;
        var absolute = delta.Duration();

        if (absolute < TimeSpan.FromSeconds(10))
            return Clamp(value.AddSeconds(10) - now);

        if (absolute < OneMinute)
            return UntilNextRelativeUnitBoundary(delta, OneSecond);

        if (absolute < OneHour)
            return UntilNextRelativeUnitBoundary(delta, OneMinute);

        if (absolute < OneDay || (delta > TimeSpan.Zero && absolute == OneDay))
            return UntilNextRelativeUnitBoundary(delta, OneHour);

        return UntilNextDay(now);
    }

    private static TimeSpan UntilNextRelativeUnitBoundary(TimeSpan delta, TimeSpan unit)
    {
        var remainderTicks = delta.Duration().Ticks % unit.Ticks;

        if (delta > TimeSpan.Zero)
            return Clamp(remainderTicks == 0 ? TimeSpan.FromMilliseconds(250) : TimeSpan.FromTicks(remainderTicks));

        return Clamp(remainderTicks == 0 ? unit : TimeSpan.FromTicks(unit.Ticks - remainderTicks));
    }

    private static TimeSpan? GetUntilInterval(DateTimeOffset value, DateTimeOffset now)
    {
        var remaining = value - now;

        if (remaining <= TimeSpan.Zero)
            return null;

        if (remaining < OneMinute)
            return OneSecond;

        if (remaining < OneHour)
            return UntilNextUnitBoundary(remaining, OneMinute);

        if (remaining < OneDay)
            return UntilNextUnitBoundary(remaining, OneHour);

        return UntilNextUnitBoundary(remaining, OneDay);
    }

    private static TimeSpan GetCalendarInterval(DateTimeOffset value, DateTimeOffset now)
    {
        var absolute = (value - now).Duration();

        if (absolute < TimeSpan.FromSeconds(10))
            return OneSecond;

        return UntilNextDay(now);
    }

    private static TimeSpan UntilNextUnitBoundary(TimeSpan remaining, TimeSpan unit)
    {
        var remainderTicks = remaining.Ticks % unit.Ticks;

        if (remainderTicks <= 0)
            return unit;

        return Clamp(TimeSpan.FromTicks(remainderTicks));
    }

    private static TimeSpan UntilNextMinute(DateTimeOffset now)
    {
        var next = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, now.Offset).AddMinutes(1);
        return Clamp(next - now);
    }

    private static TimeSpan UntilNextHour(DateTimeOffset now)
    {
        var next = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, 0, 0, now.Offset).AddHours(1);
        return Clamp(next - now);
    }

    private static TimeSpan UntilNextDay(DateTimeOffset now)
    {
        var next = new DateTimeOffset(now.Year, now.Month, now.Day, 0, 0, 0, now.Offset).AddDays(1);
        return Clamp(next - now);
    }

    private static TimeSpan Clamp(TimeSpan value)
    {
        if (value < TimeSpan.FromMilliseconds(250))
            return TimeSpan.FromMilliseconds(250);

        if (value > TimeSpan.FromDays(7))
            return TimeSpan.FromDays(7);

        return value;
    }

    private static bool FormatHasSecondPrecision(string? format)
    {
        if (string.IsNullOrWhiteSpace(format))
            return false;

        if (format.Length == 1)
            return format[0] is 'F' or 'G' or 'O' or 'o' or 's' or 'T' or 'u' or 'U';

        return ContainsUnescaped(format, 's') || ContainsUnescaped(format, 'f') || ContainsUnescaped(format, 'F');
    }

    private static bool FormatHasMinutePrecision(string? format)
    {
        if (string.IsNullOrWhiteSpace(format))
            return true;

        if (format.Length == 1)
            return format[0] is 'f' or 'F' or 'g' or 'G' or 't' or 'T' or 'O' or 'o' or 's' or 'u' or 'U';

        return ContainsUnescaped(format, 'm') || ContainsUnescaped(format, 'h') || ContainsUnescaped(format, 'H') || ContainsUnescaped(format, 't');
    }

    private static bool ContainsUnescaped(string format, char token)
    {
        var inQuote = false;
        var quote = '\0';
        var escaped = false;

        foreach (var character in format)
        {
            if (escaped)
            {
                escaped = false;
                continue;
            }

            if (character == '\\')
            {
                escaped = true;
                continue;
            }

            if (character is '\'' or '"')
            {
                if (!inQuote)
                {
                    inQuote = true;
                    quote = character;
                    continue;
                }

                if (quote == character)
                {
                    inQuote = false;
                    quote = '\0';
                    continue;
                }
            }

            if (!inQuote && character == token)
                return true;
        }

        return false;
    }
}
