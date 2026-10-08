using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a native HTML select control styled to shadcn conventions.
/// </summary>
public interface INativeSelect : IElement
{
    /// <summary>
    /// Gets or sets the ID of the owning form, including a form outside the control ancestry.
    /// </summary>
    string? Form { get; set; }

    /// <summary>
    /// Gets or sets browser autofill tokens, such as off, email, or section-shipping shipping street-address.
    /// </summary>
    string? AutoComplete { get; set; }

    /// <summary>
    /// Gets or sets the HTML size attribute.
    /// </summary>
    int? HtmlSize { get; set; }


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
    NativeSelectSize NativeSelectSize { get; set; }
}
