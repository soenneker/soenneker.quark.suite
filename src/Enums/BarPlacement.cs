using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Bar placement enumeration.
/// </summary>
[Intellenum<string>]
public sealed partial class BarPlacement
{
    public static readonly BarPlacement Top = new("top");
    public static readonly BarPlacement Bottom = new("bottom");
    public static readonly BarPlacement Fixed = new("fixed");
    public static readonly BarPlacement Sticky = new("sticky");
}
