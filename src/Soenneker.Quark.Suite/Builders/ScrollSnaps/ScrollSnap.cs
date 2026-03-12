namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll snap. Tailwind: snap-*.
/// </summary>
public static class ScrollSnap
{
    public static ScrollSnapBuilder None => new("none");
    public static ScrollSnapBuilder X => new("x");
    public static ScrollSnapBuilder Y => new("y");
    public static ScrollSnapBuilder Both => new("both");
    public static ScrollSnapBuilder Mandatory => new("mandatory");
    public static ScrollSnapBuilder Proximity => new("proximity");
}
