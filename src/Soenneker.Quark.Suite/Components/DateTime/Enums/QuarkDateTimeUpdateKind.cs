namespace Soenneker.Quark;

/// <summary>
/// Identifies the date/time presentation being formatted.
/// </summary>
public enum QuarkDateTimeUpdateKind
{
    /// <summary>
    /// A static absolute date/time value.
    /// </summary>
    Text,

    /// <summary>
    /// The current browser-local date/time.
    /// </summary>
    Now,

    /// <summary>
    /// A relative past or future phrase.
    /// </summary>
    Relative,

    /// <summary>
    /// A remaining-duration phrase until a target date/time.
    /// </summary>
    Until,

    /// <summary>
    /// A calendar-style date/time phrase.
    /// </summary>
    Calendar
}
