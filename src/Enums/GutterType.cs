using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the type of gutter spacing.
/// </summary>
[Intellenum<string>]
public sealed partial class GutterType
{
    public static readonly GutterType All = new("all");
    public static readonly GutterType X = new("x");
    public static readonly GutterType Y = new("y");
}
