using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a rule for nav styling.
/// </summary>
public readonly struct NavStyleRule
{
    public readonly NavStyleType Type;

    public NavStyleRule(NavStyleType type)
    {
        Type = type;
    }
}