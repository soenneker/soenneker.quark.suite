using System.Text.Json.Serialization;

namespace Soenneker.Quark;

internal readonly struct ThemeInteropState
{
    [JsonPropertyName("isDark")]
    public bool IsDark { get; init; }

    [JsonPropertyName("mode")]
    public string Mode { get; init; }
}
