using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a radio group container (shadcn RadioGroup). Manages value and cascades to child Radio components.
/// </summary>
public interface IRadioGroup : IElement
{
    /// <summary>
    /// Selected value (matches the Value of the selected Radio).
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Callback when the selected value changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Name used for grouping (cascaded to Radio children). If null, a unique name is generated.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Whether the whole group is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Orientation: "horizontal" or "vertical".
    /// </summary>
    string Orientation { get; set; }

    /// <summary>
    /// Aria label for the radiogroup.
    /// </summary>
    string? AriaLabel { get; set; }

    /// <summary>
    /// Gets the effective name for the group (generated if not set).
    /// </summary>
    string EffectiveName { get; }

    /// <summary>
    /// Called by child Radio when selected. Sets Value and invokes ValueChanged.
    /// </summary>
    Task SetValueAsync(string? value);
}
