using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents an individual step within a steps wizard.
/// </summary>
public interface IStep : IElement
{
    /// <summary>
    /// Gets or sets the unique name identifier for this step.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the display index of the step.
    /// </summary>
    int? Index { get; set; }

    /// <summary>
    /// Gets or sets whether the step is completed.
    /// </summary>
    bool Completed { get; set; }

    /// <summary>
    /// Gets or sets whether the step is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the background color scheme of the step.
    /// </summary>
    CssValue<ColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the status of the step.
    /// </summary>
    StepStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the step is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> OnStepClick { get; set; }

    /// <summary>
    /// Gets or sets the template for rendering a custom marker/icon for the step.
    /// </summary>
    RenderFragment? Marker { get; set; }

    /// <summary>
    /// Gets or sets the template for rendering a custom caption for the step.
    /// </summary>
    RenderFragment? Caption { get; set; }
}

