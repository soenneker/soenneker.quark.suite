using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Defines the input group textarea contract.
/// </summary>
public interface IInputGroupTextarea : IElement
{
    /// <summary>
    /// Gets or sets value.
    /// </summary>
    string? Value { get; set; }
    /// <summary>
    /// Gets or sets value changed.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }
}
