namespace Soenneker.Quark;

/// <summary>
/// Visual indicator displayed under an active navigation menu item.
/// </summary>
public interface INavigationMenuIndicator : IElement
{
    /// <summary>
    /// Gets or sets whether the indicator is visible.
    /// </summary>
    bool Visible { get; set; }
}
