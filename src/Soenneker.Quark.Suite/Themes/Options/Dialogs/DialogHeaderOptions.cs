
namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog header options.
/// </summary>
public sealed class DialogHeaderOptions : ComponentOptions
{
    public DialogHeaderOptions()
    {
        Selector = "[data-slot='dialog-header']";
    }
}
