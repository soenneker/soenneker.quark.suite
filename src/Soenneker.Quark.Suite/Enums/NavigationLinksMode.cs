using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Visual layout for navigation link collections.
/// </summary>
[EnumValue<string>]
public sealed partial class NavigationLinksMode
{
    public static readonly NavigationLinksMode Header = new("header");
    public static readonly NavigationLinksMode Stack = new("stack");
    public static readonly NavigationLinksMode Sidebar = new("sidebar");
}
