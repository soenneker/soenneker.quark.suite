using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

internal sealed class CommandEntryOrderComparer : IComparer<CommandEntry>
{
    internal static readonly CommandEntryOrderComparer Instance = new();

    public int Compare(CommandEntry? left, CommandEntry? right)
    {
        if (ReferenceEquals(left, right))
            return 0;
        if (left is null)
            return -1;
        if (right is null)
            return 1;

        var comparison = (left.Group is not null).CompareTo(right.Group is not null);
        if (comparison == 0)
            comparison = StringComparer.Ordinal.Compare(left.Group?.Heading, right.Group?.Heading);
        if (comparison == 0)
            comparison = StringComparer.OrdinalIgnoreCase.Compare(left.Item.SearchText, right.Item.SearchText);
        return comparison != 0 ? comparison : left.RegistrationOrder.CompareTo(right.RegistrationOrder);
    }
}
