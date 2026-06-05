
namespace Soenneker.Quark;

/// <summary>
/// Represents the memo input options.
/// </summary>
public sealed class MemoInputOptions : ComponentOptions
{
    public MemoInputOptions()
    {
        Selector = "[data-slot='textarea']";
    }
}
