using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents a shadcn-style accordion root that coordinates item open/close state.
/// </summary>
public interface IAccordion : IElement
{
    /// <summary>
    /// Gets or sets whether multiple items can be open simultaneously.
    /// </summary>
    bool Multiple { get; set; }

    /// <summary>
    /// Gets or sets the currently open item value for single mode.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when <see cref="Value"/> changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the currently open item values for multiple mode.
    /// </summary>
    IReadOnlyCollection<string>? Values { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when <see cref="Values"/> changes.
    /// </summary>
    EventCallback<IReadOnlyCollection<string>> ValuesChanged { get; set; }
}
