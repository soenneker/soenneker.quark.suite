using System.Text.Json.Serialization;

namespace Soenneker.Quark;

/// <summary>
/// Represents a native input selection snapshot.
/// </summary>
public sealed class InputSelectionSnapshot
{
    /// <summary>
    /// Gets or sets the selection start.
    /// </summary>
    [JsonPropertyName("start")]
    public int Start { get; set; }

    /// <summary>
    /// Gets or sets the selection end.
    /// </summary>
    [JsonPropertyName("end")]
    public int End { get; set; }

    /// <summary>
    /// Gets or sets the input value captured with the selection.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
