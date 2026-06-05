namespace Soenneker.Quark;

/// <summary>
/// Defines the input group button contract.
/// </summary>
public interface IInputGroupButton : IElement
{
    /// <summary>
    /// Gets or sets button size.
    /// </summary>
    CssValue<ButtonSizeBuilder> ButtonSize { get; set; }
    /// <summary>
    /// Gets or sets variant.
    /// </summary>
    ButtonVariant Variant { get; set; }
    /// <summary>
    /// Gets or sets type.
    /// </summary>
    ButtonType Type { get; set; }
}
