namespace Soenneker.Quark;

/// <summary>Browser gesture configuration for SwipeArea.</summary>
public sealed class SwipeAreaInteropOptions
{
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Disabled { get; set; }
    public double Threshold { get; set; }
    public int MaxDuration { get; set; }
    public string StartEdge { get; set; } = "none";
    public double EdgeSize { get; set; }
    public bool PreserveNativeEdgeGestures { get; set; }
    public bool EnableMouse { get; set; }
}
