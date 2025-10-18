namespace Soenneker.Quark;

/// <summary>
/// Interface for visual surface elements like cards, alerts, panels, and callouts.
/// Provides access to surface-related properties: borders, shadows, opacity.
/// BackgroundColor, BorderColor, and Padding are inherited from IComponent.
/// </summary>
public interface ISurfaceElement : IElement
{
    CssValue<BorderBuilder>? Border { get; set; }
    CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }
    CssValue<BoxShadowBuilder>? BoxShadow { get; set; }
    CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }
    CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }
}
