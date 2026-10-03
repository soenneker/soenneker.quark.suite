using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class CommandEntry
{
    public required CommandItem Item { get; init; }

    public CommandGroup? Group { get; set; }

    public string SearchTextSnapshot { get; set; } = string.Empty;

    public int RegistrationOrder { get; set; }

    public int SearchOrder { get; set; }

    public bool IsVisible => Item.IsVisibleResolved;

    public bool IsDisabled => Item.Disabled;

    public bool Matches(string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return true;

        return Item.SearchText.Contains(search, StringComparison.OrdinalIgnoreCase);
    }

    public Task Focus()
    {
        return Item.Focus();
    }
}
