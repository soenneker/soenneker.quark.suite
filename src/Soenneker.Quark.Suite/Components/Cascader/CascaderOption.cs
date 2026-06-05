using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the cascader option.
/// </summary>
public sealed class CascaderOption
{
    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Gets or sets label.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// Gets or sets text label.
    /// </summary>
    public string? TextLabel { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether disabled.
    /// </summary>
    public bool Disabled { get; init; }

    /// <summary>
    /// Gets or sets children.
    /// </summary>
    public IReadOnlyList<CascaderOption> Children { get; init; } = [];

    /// <summary>
    /// Gets or sets display label.
    /// </summary>
    public string DisplayLabel => string.IsNullOrWhiteSpace(TextLabel) ? Label : TextLabel!;

    /// <summary>
    /// Gets or sets a value indicating whether the instance has children.
    /// </summary>
    public bool HasChildren => Children.Count > 0;
}
