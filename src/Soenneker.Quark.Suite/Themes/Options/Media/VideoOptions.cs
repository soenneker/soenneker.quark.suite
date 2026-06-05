
namespace Soenneker.Quark;

/// <summary>
/// Represents the video options.
/// </summary>
public sealed class VideoOptions : ComponentOptions
{
    public VideoOptions()
    {
        Selector = "[data-slot='video']";
    }
}
