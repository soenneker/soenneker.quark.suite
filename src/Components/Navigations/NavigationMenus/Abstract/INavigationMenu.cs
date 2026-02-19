namespace Soenneker.Quark;

/// <summary>
/// Root container for a shadcn-style navigation menu.
/// </summary>
public interface INavigationMenu : IElement
{
    /// <summary>
    /// Gets or sets whether a default viewport should be rendered.
    /// </summary>
    bool Viewport { get; set; }
}
