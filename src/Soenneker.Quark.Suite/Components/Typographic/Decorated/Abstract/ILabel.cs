using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents a label element for form inputs.
/// </summary>
public interface ILabel : IElement
{
    /// <summary>
    /// Name of the input element to which the label is connected.
    /// </summary>
    string? For { get; set; }

    /// <summary>
    /// Gets or sets on mouse down.
    /// </summary>
    EventCallback<MouseEventArgs> OnMouseDown { get; set; }
}
