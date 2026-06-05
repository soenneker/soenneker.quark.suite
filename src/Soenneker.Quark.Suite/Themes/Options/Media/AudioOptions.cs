
namespace Soenneker.Quark;

/// <summary>
/// Represents the audio options.
/// </summary>
public sealed class AudioOptions : ComponentOptions
{
    public AudioOptions()
    {
        Selector = "[data-slot='audio']";
    }
}
