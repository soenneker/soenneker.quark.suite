using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>shadcn TabsList variant: default (bg-muted pill) or line (underline).</summary>
[EnumValue<string>]
public sealed partial class TabsListVariant
{
    public static readonly TabsListVariant Default = new("default");
    public static readonly TabsListVariant Line = new("line");
}
