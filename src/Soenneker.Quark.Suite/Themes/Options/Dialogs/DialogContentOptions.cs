
namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog content options.
/// </summary>
public sealed class DialogContentOptions : ComponentOptions
{
    public DialogContentOptions()
    {
        Selector = "[data-slot='dialog-content']";
    }
}
