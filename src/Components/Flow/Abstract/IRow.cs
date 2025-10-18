namespace Soenneker.Quark;

/// <summary>
/// Represents a Bootstrap grid row component.
/// </summary>
public interface IRow : IElement
{
    /// <summary>
    /// Gets or sets the gutter configuration for the row.
    /// </summary>
    CssValue<GutterBuilder>? Gutter { get; set; }
}