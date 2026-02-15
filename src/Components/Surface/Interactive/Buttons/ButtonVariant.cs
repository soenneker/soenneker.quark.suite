namespace Soenneker.Quark;

/// <summary>
/// Defines the visual style variant for a Button component.
/// Follows shadcn/ui design system using CSS custom properties.
/// </summary>
public enum ButtonVariant
{
    /// <summary>Default primary button style (solid background).</summary>
    Default,

    /// <summary>Destructive action style (delete, remove).</summary>
    Destructive,

    /// <summary>Outlined style with transparent background and border.</summary>
    Outline,

    /// <summary>Secondary style with muted background.</summary>
    Secondary,

    /// <summary>Ghost style - no background, shows on hover.</summary>
    Ghost,

    /// <summary>Link-styled button (underlined text).</summary>
    Link
}
