using System.Buffers;
using System.Text;
using System.Text.Json;

namespace Soenneker.Quark;

internal static class CodeEditorOptionsJson
{
    internal static string Create(string? text, string language, string theme, bool readOnly, bool wordWrap, bool minimap)
    {
        // Write the fixed Monaco payload without reflection metadata, which can be removed by WASM trimming.
        var buffer = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(buffer))
        {
            writer.WriteStartObject();
            writer.WriteString("value", text ?? string.Empty);
            writer.WriteString("language", language);
            writer.WriteString("theme", theme);
            writer.WriteBoolean("readOnly", readOnly);
            writer.WriteString("wordWrap", wordWrap ? "on" : "off");
            writer.WriteStartObject("minimap");
            writer.WriteBoolean("enabled", minimap);
            writer.WriteEndObject();
            writer.WriteBoolean("automaticLayout", true);
            writer.WriteEndObject();
        }

        return Encoding.UTF8.GetString(buffer.WrittenSpan);
    }
}
