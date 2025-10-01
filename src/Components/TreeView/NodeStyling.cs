namespace Soenneker.Quark;

/// <summary>
/// Represents styling options for TreeView nodes.
/// </summary>
public sealed class NodeStyling
{
    /// <summary>
    /// Gets or sets the background color for the node.
    /// </summary>
    public CssValue<ColorBuilder> Background { get; set; } = Quark.Color.Initial;

    /// <summary>
    /// Gets or sets the text color for the node.
    /// </summary>
    public CssValue<ColorBuilder> TextColor { get; set; } = Quark.Color.Initial;

    /// <summary>
    /// Gets or sets the CSS class for the node.
    /// </summary>
    public string Class { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the inline style for the node.
    /// </summary>
    public string Style { get; set; } = string.Empty;
}
