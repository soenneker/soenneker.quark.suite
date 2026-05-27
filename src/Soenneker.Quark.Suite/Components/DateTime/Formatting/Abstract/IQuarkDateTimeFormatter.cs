using System;

namespace Soenneker.Quark;

/// <summary>
/// Formats Quark date/time component values.
/// </summary>
public interface IQuarkDateTimeFormatter
{
    /// <summary>
    /// Converts a value to the resolved display time zone.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The converted value.</returns>
    DateTimeOffset Convert(DateTimeOffset value, QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Formats an absolute date/time value.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The formatted date/time text.</returns>
    string Format(DateTimeOffset value, QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Formats a relative past or future phrase.
    /// </summary>
    /// <param name="value">The target value.</param>
    /// <param name="now">The current instant.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The relative text.</returns>
    string FormatRelative(DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Formats the remaining time until a target value.
    /// </summary>
    /// <param name="value">The target value.</param>
    /// <param name="now">The current instant.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The remaining-time text.</returns>
    string FormatUntil(DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Formats a calendar-style phrase.
    /// </summary>
    /// <param name="value">The target value.</param>
    /// <param name="now">The current instant.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The calendar text.</returns>
    string FormatCalendar(DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Formats a full readable title for a value.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The title text.</returns>
    string FormatTitle(DateTimeOffset value, QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Formats a semantic ISO 8601 datetime attribute value.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The datetime attribute text.</returns>
    string FormatDateTimeAttribute(DateTimeOffset value, QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Gets the next useful automatic update interval for a formatted value.
    /// </summary>
    /// <param name="kind">The formatted value kind.</param>
    /// <param name="value">The target value.</param>
    /// <param name="now">The current instant.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The next interval, or <c>null</c> when no automatic update is needed.</returns>
    TimeSpan? GetNextUpdateInterval(QuarkDateTimeUpdateKind kind, DateTimeOffset value, DateTimeOffset now, QuarkDateTimeFormatOptions options);
}
