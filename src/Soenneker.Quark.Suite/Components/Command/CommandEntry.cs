using System;

namespace Soenneker.Quark;

internal sealed class CommandEntry
{
    public required CommandItem Item { get; init; }

    public CommandGroup? Group { get; init; }

    public bool IsVisible => Item.IsVisibleResolved;

    public bool IsDisabled => Item.Disabled;

    public bool Matches(string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return true;

        return Item.SearchText.Contains(search, StringComparison.OrdinalIgnoreCase);
    }
}
