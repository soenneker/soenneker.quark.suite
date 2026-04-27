using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Badge visual style variant following shadcn/ui design system.
/// </summary>
[EnumValue]
public sealed partial class BadgeVariant
{
    public static readonly BadgeVariant Default = new(0);
    public static readonly BadgeVariant Secondary = new(1);
    public static readonly BadgeVariant Destructive = new(2);
    public static readonly BadgeVariant Outline = new(3);
    public static readonly BadgeVariant Ghost = new(4);
    public static readonly BadgeVariant Link = new(5);
}
