namespace Soenneker.Quark;

/// <summary>
/// Represents a collapsible menu container within a navigation bar.
/// </summary>
public interface IBarMenu : IElement
{
    /// <summary>
    /// Gets or sets whether the menu should be aligned to the right.
    /// </summary>
    bool Right { get; set; }
}

