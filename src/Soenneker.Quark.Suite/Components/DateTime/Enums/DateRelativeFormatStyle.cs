using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Controls how relative date/time values are displayed.
/// </summary>
[EnumValue]
public sealed partial class DateRelativeFormatStyle
{
    /// <summary>
    /// Displays full relative phrases, such as "5 hours ago" or "in 2 months".
    /// </summary>
    public static readonly DateRelativeFormatStyle Long = new(0);

    /// <summary>
    /// Displays shortened relative values, such as "5 hr", "5 mo", or "2 min".
    /// </summary>
    public static readonly DateRelativeFormatStyle Short = new(1);
}

