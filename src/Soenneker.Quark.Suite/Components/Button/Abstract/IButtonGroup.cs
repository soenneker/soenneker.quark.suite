namespace Soenneker.Quark;

/// <summary>
/// Represents a button group component for grouping related buttons together.
/// </summary>
public interface IButtonGroup : IElement
{
    /// <summary>
    /// Gets or sets the size of all buttons in the group (shadcn: default, sm, lg, icon, etc.).
    /// </summary>
    CssValue<ButtonSizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether the button group should be displayed vertically.
    /// </summary>
    bool Vertical { get; set; }

}
