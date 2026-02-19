namespace Soenneker.Quark;

/// <summary>
/// Trigger button used to open a navigation menu item.
/// </summary>
public interface INavigationMenuTrigger : IElement
{
    /// <summary>
    /// Gets or sets whether the trigger is in an open state.
    /// </summary>
    bool Open { get; set; }

    /// <summary>
    /// Gets or sets whether the trigger is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the chevron icon is displayed.
    /// </summary>
    bool ShowChevron { get; set; }
}
