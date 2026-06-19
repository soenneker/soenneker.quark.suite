using System.Globalization;

namespace Soenneker.Quark;

/// <summary>
/// Provides options used by Quark date/time formatters.
/// </summary>
public sealed class QuarkDateTimeFormatOptions
{
    /// <summary>
    /// Gets or sets an optional .NET date/time format string.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Gets or sets the requested time zone identifier.
    /// </summary>
    public string? TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the detected browser time zone identifier.
    /// </summary>
    public string? BrowserTimeZone { get; set; }

    /// <summary>
    /// Gets or sets the culture used for localized date/time formatting.
    /// </summary>
    public CultureInfo? Culture { get; set; }

    /// <summary>
    /// Gets or sets an optional culture name used when <see cref="Culture"/> is not supplied.
    /// </summary>
    public string? CultureName { get; set; }

    /// <summary>
    /// Gets or sets the text rendered when a value is null.
    /// </summary>
    public string? NullText { get; set; }

    /// <summary>
    /// Gets or sets how relative date/time values are displayed.
    /// </summary>
    public DateRelativeFormatStyle DateRelativeFormatStyle { get; set; } = DateRelativeFormatStyle.Long;

    /// <summary>
    /// Gets or sets the text rendered by countdown components after the target has passed.
    /// </summary>
    public string? ExpiredText { get; set; }
}
