namespace Soenneker.Quark;

public interface IInputGroupButton : IElement
{
    ButtonSize ButtonSize { get; set; }
    ButtonVariant Variant { get; set; }
    ButtonType Type { get; set; }
}
