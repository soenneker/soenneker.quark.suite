using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;

namespace Soenneker.Quark;

/// <summary>
/// Represents a tabbed navigation component for organizing content into tabs.
/// </summary>
public interface ITabs : IElement
{
    /// <summary>
    /// Legacy compatibility alias for the shadcn default tab-list style. shadcn v4 exposes <see cref="TabsListVariant.Default"/> and <see cref="TabsListVariant.Line"/>, not a separate pills variant.
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
    /// Radix <c>activationMode</c>: automatic activates the focused tab; manual moves focus with arrows and activates with Space/Enter or click.
    /// </summary>
    TabsActivationMode ActivationMode { get; set; }

    /// <summary>
    /// Radix <c>loop</c>: when <c>false</c>, arrow keys do not wrap from first/last tab.
    /// </summary>
    bool Loop { get; set; }

    /// <summary>
    /// Gets or sets the currently selected tab name.
    /// </summary>
    string? SelectedTab { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked When the selected tab changes.
    /// </summary>
    EventCallback<string> OnSelectedTabChanged { get; set; }
}
