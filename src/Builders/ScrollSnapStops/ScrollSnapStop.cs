namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll snap stop. Tailwind: snap-stop-normal, snap-stop-always.
/// </summary>
public static class ScrollSnapStop
{
    public static ScrollSnapStopBuilder Normal => new("normal");
    public static ScrollSnapStopBuilder Always => new("always");
}
