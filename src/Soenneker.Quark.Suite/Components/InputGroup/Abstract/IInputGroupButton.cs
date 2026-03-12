using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

public interface IInputGroupButton : IElement
{
    ButtonSize Size { get; set; }
    ButtonVariant Variant { get; set; }
    ButtonType Type { get; set; }
}
