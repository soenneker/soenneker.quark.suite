namespace Soenneker.Quark;

/// <summary>Options passed to the scroll reveal JavaScript module.</summary>
public sealed class ScrollRevealInteropOptions
{
    /// <summary>Gets or sets the intersection threshold.</summary>
    public double Threshold { get; set; }

    /// <summary>Gets or sets the intersection root margin.</summary>
    public string? RootMargin { get; set; }

    /// <summary>Gets or sets whether content is revealed only once.</summary>
    public bool Once { get; set; }

    /// <summary>Gets or sets whether reveal is disabled.</summary>
    public bool Disabled { get; set; }
}
