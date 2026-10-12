using System.Collections.Generic;

namespace Soenneker.Quark;

internal sealed class CommandNavigationOrderComparer : IComparer<CommandEntry>
{
    internal static readonly CommandNavigationOrderComparer Instance = new();

    public int Compare(CommandEntry? left, CommandEntry? right)
    {
        if (ReferenceEquals(left, right)) return 0;
        if (left is null) return -1;
        if (right is null) return 1;
        var comparison = left.NavigationGroupOrder.CompareTo(right.NavigationGroupOrder);
        if (comparison == 0) comparison = left.SearchOrder.CompareTo(right.SearchOrder);
        return comparison != 0 ? comparison : left.RegistrationOrder.CompareTo(right.RegistrationOrder);
    }
}
