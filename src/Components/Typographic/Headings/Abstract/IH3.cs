namespace Soenneker.Quark;

/// <summary>
/// Represents a level 3 heading element (h3).
/// </summary>
public interface IH3 : IElement
{
    /// <summary>
    /// Gets or sets the display size for Bootstrap display headings (display-1 through display-6).
    /// </summary>
    CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }
}

