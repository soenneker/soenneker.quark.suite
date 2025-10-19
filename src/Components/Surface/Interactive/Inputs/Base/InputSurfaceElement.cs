using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Shared base for input components with surface visuals.
/// Provides common input properties like background, border, padding, disabled, and readonly states.
/// </summary>
public abstract class InputSurfaceElement : InteractiveSurfaceElement, IInputSurfaceElement
{
    /// <summary>
    /// Gets or sets whether the input is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the input is read-only.
    /// </summary>
    [Parameter]
    public bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets the name attribute for form submission.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text displayed when the input is empty.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the input is required.
    /// </summary>
    [Parameter]
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets whether the input should automatically receive focus when the page loads.
    /// </summary>
    [Parameter]
    public bool AutoFocus { get; set; }
}