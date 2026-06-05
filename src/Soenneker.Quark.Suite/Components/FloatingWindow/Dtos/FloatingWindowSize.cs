using System.Text.Json.Serialization;

namespace Soenneker.Quark;

/// <summary>
/// Represents the floating window size.
/// </summary>
public sealed class FloatingWindowSize
{
    /// <summary>
    /// Gets or sets width.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets height.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
}
