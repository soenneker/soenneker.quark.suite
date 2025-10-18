namespace Soenneker.Quark;

/// <summary>
/// Represents an individual tab within a tabs component.
/// </summary>
public interface ITab : IElement
{
    /// <summary>
    /// Gets or sets the unique name/identifier of the tab.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the display title of the tab.
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Gets or sets the icon name to display in the tab.
    /// </summary>
    string? IconName { get; set; }

    /// <summary>
    /// Gets or sets the target ID of the tab panel this tab controls.
    /// </summary>
    string? Target { get; set; }

    /// <summary>
    /// Gets or sets whether this tab is currently active/selected.
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether this tab is disabled.
    /// </summary>
    bool Disabled { get; set; }
}

