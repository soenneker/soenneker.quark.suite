
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for code component styling.
/// </summary>
public sealed class CodeOptions : ComponentOptions
{
    public CodeOptions()
    {
        Selector = "[data-slot='code']";
    }
}
