using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the type of column auto.
/// </summary>
[Intellenum<string>]
public sealed partial class ColumnAutoType
{
    /// <summary>
    /// Auto-sizing column that takes remaining space.
    /// </summary>
    public static readonly ColumnAutoType Auto = new("auto");
}
