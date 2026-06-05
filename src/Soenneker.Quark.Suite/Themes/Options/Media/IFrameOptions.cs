
namespace Soenneker.Quark;

/// <summary>
/// Represents the i frame options.
/// </summary>
public sealed class IFrameOptions : ComponentOptions
{
    public IFrameOptions()
    {
        Selector = "[data-slot='iframe']";
    }
}
