using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the visual style variant for a Button component.
/// Follows shadcn/ui design system using CSS custom properties.
/// </summary>
[EnumValue<string>]
public sealed partial class ButtonVariant
{
    /// <summary>Default primary button style (solid background).</summary>
    public static readonly ButtonVariant Default = new("default");

    /// <summary>Destructive action style (delete, remove).</summary>
    public static readonly ButtonVariant Destructive = new("destructive");

    /// <summary>Outlined style with transparent background and border.</summary>
    public static readonly ButtonVariant Outline = new("outline");

    /// <summary>Secondary style with muted background.</summary>
    public static readonly ButtonVariant Secondary = new("secondary");

    /// <summary>Ghost style - no background, shows on hover.</summary>
    public static readonly ButtonVariant Ghost = new("ghost");

    /// <summary>Link-styled button (underlined text).</summary>
    public static readonly ButtonVariant Link = new("link");
}
