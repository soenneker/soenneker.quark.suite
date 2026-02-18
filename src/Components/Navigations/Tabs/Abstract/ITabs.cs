using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a tabbed navigation component for organizing content into tabs.
/// </summary>
public interface ITabs : IElement
{
    /// <summary>
    /// Gets or sets whether the tabs should be styled as pills instead of tabs.
    /// </summary>
    bool Pills { get; set; }

    /// <summary>
    /// Gets or sets whether the tabs should be displayed vertically.
    /// </summary>
    bool Vertical { get; set; }

    /// <summary>
    /// Gets or sets whether the tabs should take up the full width (justified).
    /// </summary>
    bool Justified { get; set; }

    /// <summary>
    /// Gets or sets the TabsList variant (default or line).
    /// </summary>
    TabsListVariant TabsListVariant { get; set; }

    /// <summary>
    /// Gets or sets the currently selected tab name.
    /// </summary>
    string? SelectedTab { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked When the selected tab changes.
    /// </summary>
    EventCallback<string> OnSelectedTabChanged { get; set; }
}

