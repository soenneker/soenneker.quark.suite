
namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog close button options.
/// </summary>
public sealed class DialogCloseButtonOptions : ComponentOptions
{
    public DialogCloseButtonOptions()
    {
        Selector = "[data-slot='dialog-close']";
    }
}
