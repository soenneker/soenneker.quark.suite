namespace Soenneker.Quark;

/// <summary>
/// Represents a level 1 heading element (h1).
/// </summary>
public interface IH1 : IElement
{
    /// <summary>
    /// Gets or sets the display size for Bootstrap display headings (display-1 through display-6).
    /// </summary>
    CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }
}

