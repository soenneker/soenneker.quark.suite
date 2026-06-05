using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Defines the input group input contract.
/// </summary>
public interface IInputGroupInput : IElement
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
