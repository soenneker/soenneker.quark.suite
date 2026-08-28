namespace Soenneker.Quark;

/// <summary>Describes a two-stop SVG gradient.</summary>
public sealed record ChartGradient(string StartColor, string EndColor)
{
    /// <summary>Gets the opacity of the start stop.</summary>
    public double StartOpacity { get; init; } = 0.8;

    /// <summary>Gets the opacity of the end stop.</summary>
    public double EndOpacity { get; init; } = 0.08;

    /// <summary>Gets whether the gradient runs horizontally instead of vertically.</summary>
    public bool Horizontal { get; init; }
}
