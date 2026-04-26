namespace Soenneker.Quark;

/// <summary>
/// Represents styling options for TreeView nodes.
/// </summary>
public sealed class NodeStyling
{
    /// <summary>
    /// Gets or sets the background color for the node.
    /// </summary>
    public CssValue<BackgroundColorBuilder> Background { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the TextColor color for the node.
    /// </summary>
    public CssValue<TextColorBuilder> TextColor { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the CSS class for the node.
    /// </summary>
    public string Class { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the inline style for the node.
    /// </summary>
    public string Style { get; set; } = string.Empty;
}
