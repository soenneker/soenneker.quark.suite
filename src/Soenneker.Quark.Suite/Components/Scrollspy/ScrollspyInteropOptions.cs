namespace Soenneker.Quark;

/// <summary>Options passed to the scrollspy JavaScript module.</summary>
public sealed class ScrollspyInteropOptions
{
    /// <summary>Gets or sets the target container selector.</summary>
    public string? TargetSelector { get; set; }

    /// <summary>Gets or sets the scroll offset in pixels.</summary>
    public int Offset { get; set; }

    /// <summary>Gets or sets whether scrolling is smooth.</summary>
    public bool Smooth { get; set; } = true;

    /// <summary>Gets or sets the link data attribute.</summary>
    public string DataAttribute { get; set; } = "scrollspy";

    /// <summary>Gets or sets whether navigation updates history.</summary>
    public bool History { get; set; } = true;

    /// <summary>Gets or sets the update throttle interval in milliseconds.</summary>
    public int ThrottleTime { get; set; }
}
