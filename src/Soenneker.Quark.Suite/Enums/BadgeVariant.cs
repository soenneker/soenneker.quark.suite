using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Badge visual style variant following shadcn/ui design system.
/// </summary>
[EnumValue<string>]
public sealed partial class BadgeVariant
{
    public static readonly BadgeVariant Default = new("default");
    public static readonly BadgeVariant Secondary = new("secondary");
    public static readonly BadgeVariant Destructive = new("destructive");
    public static readonly BadgeVariant Outline = new("outline");
    public static readonly BadgeVariant Ghost = new("ghost");
    public static readonly BadgeVariant Link = new("link");
}
