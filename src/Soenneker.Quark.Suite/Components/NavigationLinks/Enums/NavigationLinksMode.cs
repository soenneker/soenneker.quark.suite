using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Visual layout for navigation link collections.
/// </summary>
[EnumValue]
public sealed partial class NavigationLinksMode
{
    /// <summary>
    /// The header.
    /// </summary>
    public static readonly NavigationLinksMode Header = new(0);
    /// <summary>
    /// The stack.
    /// </summary>
    public static readonly NavigationLinksMode Stack = new(1);
    /// <summary>
    /// The sidebar.
    /// </summary>
    public static readonly NavigationLinksMode Sidebar = new(2);
}
