using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Soenneker.Quark;

internal static class ConsoleJsonFormatter
{
    internal static ConsoleJsonContent Format(string text)
    {
        StringBuilder? result = null;
        var copied = 0;
        List<string> payloads = [];
        for (var start = 0; start < text.Length; start++)
        {
            char first = text[start];
            if (first is not ('"' or '{' or '[')) continue;

            var end = FindEnd(text, start);
            if (end < 0) continue;
            try
            {
                using JsonDocument candidate = JsonDocument.Parse(text.AsMemory(start, end - start + 1));
                using JsonDocument? decoded = candidate.RootElement.ValueKind == JsonValueKind.String
                    ? ParseContainer(candidate.RootElement.GetString()!) : null;
                JsonElement value = decoded?.RootElement ?? candidate.RootElement;
                if (value.ValueKind is JsonValueKind.Object or JsonValueKind.Array)
                {
                    using var stream = new MemoryStream();
                    using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
                        value.WriteTo(writer);
                    result ??= new StringBuilder(text.Length);
                    result.Append(text, copied, start - copied);
                    result.Append(value.ValueKind == JsonValueKind.Object ? "{ … }" : "[ … ]");
                    payloads.Add(Encoding.UTF8.GetString(stream.ToArray()));
                    copied = end + 1;
                }
            }
            catch (JsonException)
            {
                // Ordinary brackets, quoted messages and malformed payloads remain unchanged.
            }
            start = end;
        }

        return new ConsoleJsonContent(result is null ? text : result.Append(text, copied, text.Length - copied).ToString(), payloads);
    }

    private static JsonDocument? ParseContainer(string text)
    {
        ReadOnlySpan<char> trimmed = text.AsSpan().TrimStart();
        return !trimmed.IsEmpty && trimmed[0] is '{' or '[' ? JsonDocument.Parse(text) : null;
    }

    private static int FindEnd(string text, int start)
    {
        bool quoted = text[start] == '"';
        var depth = quoted ? 0 : 1;
        for (var i = start + 1; i < text.Length; i++)
        {
            char c = text[i];
            if (quoted)
            {
                if (c == '\\') i++;
                else if (c == '"')
                {
                    quoted = false;
                    if (depth == 0) return i;
                }
            }
            else if (c == '"') quoted = true;
            else if (c is '{' or '[') depth++;
            else if (c is '}' or ']')
            {
                if (--depth == 0) return i;
            }
        }
        return -1;
    }
}
