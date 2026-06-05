using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>shadcn TabsList variant: default (bg-muted pill) or line (underline).</summary>
[EnumValue]
public sealed partial class TabsListVariant
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly TabsListVariant Default = new(0);
    /// <summary>
    /// The line.
    /// </summary>
    public static readonly TabsListVariant Line = new(1);
}
