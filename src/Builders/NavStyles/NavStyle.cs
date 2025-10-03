using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap nav utilities.
/// </summary>
public readonly struct NavStyle
{
    public readonly NavStyleType Type;

    public NavStyle(NavStyleType type)
    {
        Type = type;
    }

    public static NavStyle Default => new(NavStyleType.Default);
    public static NavStyle Tabs => new(NavStyleType.Tabs);
    public static NavStyle Pills => new(NavStyleType.Pills);
    public static NavStyle Justified => new(NavStyleType.Justified);
    public static NavStyle Fill => new(NavStyleType.Fill);
}