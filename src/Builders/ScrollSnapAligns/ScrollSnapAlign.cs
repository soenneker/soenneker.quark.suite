namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll snap align. Tailwind: snap-start, snap-center, snap-end, snap-align-none.
/// </summary>
public static class ScrollSnapAlign
{
    public static ScrollSnapAlignBuilder Start => new("start");
    public static ScrollSnapAlignBuilder Center => new("center");
    public static ScrollSnapAlignBuilder End => new("end");
    public static ScrollSnapAlignBuilder None => new("none");
}
