using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the type of column auto.
/// </summary>
[Intellenum<string>]
public sealed partial class ColumnAutoType
{
    public static readonly ColumnAutoType Auto = new("auto");
}
