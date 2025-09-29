using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Hints at the type of data that might be entered by the user while editing the DateEdit component.
/// </summary>
[Intellenum<string>]
public sealed partial class DateInputMode
{
    /// <summary>
    /// Only date is allowed to be entered.
    /// </summary>
    public static readonly DateInputMode Date = new("date");

    /// <summary>
    /// Both date and time are allowed to be entered.
    /// </summary>
    public static readonly DateInputMode DateTime = new("datetime-local");

    /// <summary>
    /// Allowed to select only year and month.
    /// 
    /// Note that not all browser supports this mode.
    /// </summary>
    public static readonly DateInputMode Month = new("month");
}
