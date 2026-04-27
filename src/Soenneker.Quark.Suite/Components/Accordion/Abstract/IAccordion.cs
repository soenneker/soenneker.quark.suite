using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents a shadcn-style accordion root that coordinates item open/close state.
/// </summary>
public interface IAccordion : IElement
{
    /// <summary>
    /// Convenience flag that resolves the accordion to Radix multiple mode when set.
    /// </summary>
    bool Multiple { get; set; }

    /// <summary>
    /// Gets or sets whether multiple items can be open simultaneously.
    /// </summary>
    SelectionMode Type { get; set; }

    /// <summary>
    /// Gets or sets whether an open item can be collapsed by activating its trigger again.
    /// </summary>
    bool Collapsible { get; set; }

    /// <summary>
    /// When true, the entire accordion is non-interactive (Radix <c>disabled</c> on root).
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Layout orientation (Radix: vertical uses ArrowUp/ArrowDown; horizontal uses ArrowLeft/ArrowRight with RTL mirroring).
    /// </summary>
    Orientation Orientation { get; set; }

    /// <summary>
    /// Gets or sets the currently open item value for single mode.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the initially open item value for uncontrolled single mode.
    /// </summary>
    string? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when <see cref="Value"/> changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the currently open item values for multiple mode.
    /// </summary>
    IReadOnlyCollection<string>? Values { get; set; }

    /// <summary>
    /// Gets or sets the initially open item values for uncontrolled multiple mode.
    /// </summary>
    IReadOnlyCollection<string>? DefaultValues { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when <see cref="Values"/> changes.
    /// </summary>
    EventCallback<IReadOnlyCollection<string>> ValuesChanged { get; set; }
}
