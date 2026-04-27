using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>shadcn TabsList variant: default (bg-muted pill) or line (underline).</summary>
[EnumValue]
public sealed partial class TabsListVariant
{
    public static readonly TabsListVariant Default = new(0);
    public static readonly TabsListVariant Line = new(1);
}
