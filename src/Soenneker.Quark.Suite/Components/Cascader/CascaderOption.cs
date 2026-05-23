using System.Collections.Generic;

namespace Soenneker.Quark;

public sealed class CascaderOption
{
    public required string Value { get; init; }

    public required string Label { get; init; }

    public string? TextLabel { get; init; }

    public bool Disabled { get; init; }

    public IReadOnlyList<CascaderOption> Children { get; init; } = [];

    public string DisplayLabel => string.IsNullOrWhiteSpace(TextLabel) ? Label : TextLabel!;

    public bool HasChildren => Children.Count > 0;
}
