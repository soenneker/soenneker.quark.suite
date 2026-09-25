namespace Soenneker.Quark;

/// <summary>Options used to collect table of contents headings.</summary>
public sealed class OnThisPageInteropOptions
{
    /// <summary>Gets or sets the content root selector.</summary>
    public string? RootSelector { get; set; }

    /// <summary>Gets or sets the heading selector.</summary>
    public string? HeadingSelector { get; set; }

    /// <summary>Gets or sets the selector for excluded headings.</summary>
    public string? IgnoreSelector { get; set; }

    /// <summary>Gets or sets the observer root margin.</summary>
    public string? RootMargin { get; set; }
}
