using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents the main layout container component for structuring page layouts.
/// </summary>
public interface ILayout : IElement
{
    /// <summary>
    /// Gets or sets whether the layout includes a sidebar.
    /// </summary>
    bool Sider { get; set; }

    /// <summary>
    /// Gets or sets whether the layout is in a loading state.
    /// </summary>
    bool Loading { get; set; }

    /// <summary>
    /// Gets or sets the CSS class for the loading overlay.
    /// </summary>
    string? LoadingClass { get; set; }

    /// <summary>
    /// Gets or sets the template to display When loading.
    /// </summary>
    RenderFragment? LoadingTemplate { get; set; }
}

