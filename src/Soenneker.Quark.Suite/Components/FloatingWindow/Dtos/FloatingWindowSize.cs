using System.Text.Json.Serialization;

namespace Soenneker.Quark;

public sealed class FloatingWindowSize
{
    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}
