
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for code chip component styling.
/// </summary>
public sealed class CodeChipOptions : ComponentOptions
{
    public CodeChipOptions()
    {
        Selector = "[data-slot='code-chip']";
    }
}
