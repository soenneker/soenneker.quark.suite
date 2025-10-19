namespace Soenneker.Quark;

/// <summary>
/// Represents a level 2 heading element (h2).
/// </summary>
public interface IH2 : IElement
{
    /// <summary>
    /// Gets or sets the display size for Bootstrap display headings (display-1 through display-6).
    /// </summary>
    CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }
}

