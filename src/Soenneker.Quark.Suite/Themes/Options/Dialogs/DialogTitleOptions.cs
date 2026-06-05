
namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog title options.
/// </summary>
public sealed class DialogTitleOptions : ComponentOptions
{
    public DialogTitleOptions()
    {
        Selector = "[data-slot='dialog-title']";
    }
}
