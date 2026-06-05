
namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog footer options.
/// </summary>
public sealed class DialogFooterOptions : ComponentOptions
{
    public DialogFooterOptions()
    {
        Selector = "[data-slot='dialog-footer']";
    }
}
