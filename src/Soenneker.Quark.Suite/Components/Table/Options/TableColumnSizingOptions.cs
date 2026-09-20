namespace Soenneker.Quark;

/// <summary>Controls content-based column sizing when adaptive layout is enabled.</summary>
public sealed record TableColumnSizingOptions
{
    /// <summary>Extra horizontal space, in pixels, reserved beyond measured content. Defaults to 24.</summary>
    public double Padding { get; init; } = 24;

    /// <summary>Minimum measured column allocation, in pixels. Defaults to 64.</summary>
    public double MinWidth { get; init; } = 64;

    /// <summary>
    /// Maximum measured column allocation, in pixels, before content wraps. Defaults to 640.
    /// The widest column can receive additional space to fill the table container.
    /// Narrow containers scroll horizontally instead of squeezing every column.
    /// </summary>
    public double MaxWidth { get; init; } = 640;
}
