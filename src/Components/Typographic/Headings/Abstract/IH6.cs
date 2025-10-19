namespace Soenneker.Quark;

/// <summary>
/// Represents a level 6 heading element (h6).
/// </summary>
public interface IH6 : IElement
{
    /// <summary>
    /// Gets or sets the display size for Bootstrap display headings (display-1 through display-6).
    /// </summary>
    CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }
}

