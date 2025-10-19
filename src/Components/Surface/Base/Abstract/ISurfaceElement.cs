namespace Soenneker.Quark;

/// <summary>
/// Interface for visual surface elements like cards, alerts, panels, and callouts.
/// Provides access to surface-related properties: borders, shadows, opacity.
/// BackgroundColor, BorderColor, and Padding are inherited from IComponent.
/// </summary>
public interface ISurfaceElement : ISurface, IElement
{
}
