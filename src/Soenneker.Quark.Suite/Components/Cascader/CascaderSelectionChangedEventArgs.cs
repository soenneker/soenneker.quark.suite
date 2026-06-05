using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the cascader selection changed event args.
/// </summary>
public sealed class CascaderSelectionChangedEventArgs
{
    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public required string[] Value { get; init; }

    /// <summary>
    /// Gets or sets selected options.
    /// </summary>
    public required IReadOnlyList<CascaderOption> SelectedOptions { get; init; }
}
