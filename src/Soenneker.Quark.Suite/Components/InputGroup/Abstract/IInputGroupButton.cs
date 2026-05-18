namespace Soenneker.Quark;

public interface IInputGroupButton : IElement
{
    CssValue<ButtonSizeBuilder> ButtonSize { get; set; }
    ButtonVariant Variant { get; set; }
    ButtonType Type { get; set; }
}
