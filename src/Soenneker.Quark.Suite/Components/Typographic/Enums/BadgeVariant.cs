using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Badge visual style variant following shadcn/ui design system.
/// </summary>
[EnumValue]
public sealed partial class BadgeVariant
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly BadgeVariant Default = new(0);
    /// <summary>
    /// The secondary.
    /// </summary>
    public static readonly BadgeVariant Secondary = new(1);
    /// <summary>
    /// The destructive.
    /// </summary>
    public static readonly BadgeVariant Destructive = new(2);
    /// <summary>
    /// The outline.
    /// </summary>
    public static readonly BadgeVariant Outline = new(3);
    /// <summary>
    /// The ghost.
    /// </summary>
    public static readonly BadgeVariant Ghost = new(4);
    /// <summary>
    /// The link.
    /// </summary>
    public static readonly BadgeVariant Link = new(5);
}
