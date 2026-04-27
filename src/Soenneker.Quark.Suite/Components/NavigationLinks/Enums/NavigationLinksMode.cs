using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Visual layout for navigation link collections.
/// </summary>
[EnumValue]
public sealed partial class NavigationLinksMode
{
    public static readonly NavigationLinksMode Header = new(0);
    public static readonly NavigationLinksMode Stack = new(1);
    public static readonly NavigationLinksMode Sidebar = new(2);
}
