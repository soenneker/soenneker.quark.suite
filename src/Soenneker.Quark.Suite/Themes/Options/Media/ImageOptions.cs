
namespace Soenneker.Quark;

/// <summary>
/// Represents the image options.
/// </summary>
public sealed class ImageOptions : ComponentOptions
{
    public ImageOptions()
    {
        Selector = "[data-slot='image']";
    }
}
