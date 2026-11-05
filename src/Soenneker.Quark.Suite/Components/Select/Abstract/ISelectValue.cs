using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Displays a Select's current value or placeholder.</summary>
public interface ISelectValue : IElement
{
    /// <summary>The text shown when no value is selected.</summary>
    string? Placeholder { get; set; }

    /// <summary>Renders rich content for the selected string representation. Explicit ChildContent takes precedence; item Text still supplies typeahead and native form text.</summary>
    RenderFragment<string?>? ValueTemplate { get; set; }
}
