using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a native HTML select control styled to shadcn conventions.
/// </summary>
public interface INativeSelect : IElement
{
    /// <summary>
    /// Gets or sets the selected value.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets callback invoked when selected value changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the control size.
    /// </summary>
    NativeSelectSize Size { get; set; }
}
