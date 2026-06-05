
namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog body options.
/// </summary>
public sealed class DialogBodyOptions : ComponentOptions
{
    public DialogBodyOptions()
    {
        Selector = "[data-slot='dialog-body']";
    }
}
