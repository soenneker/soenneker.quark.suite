using System;
namespace Soenneker.Quark;
/// <summary>Validates editor link protocols before content is passed to the browser.</summary>
public static class RichTextUrl
{
    public static bool IsSafe(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        value = value.Trim();
        foreach (var c in value) if (char.IsControl(c) || c == '\\') return false;
        if (value.StartsWith("//", StringComparison.Ordinal)) return false;
        if (Uri.TryCreate(value, UriKind.Absolute, out var uri)) return uri.Scheme is "http" or "https" or "mailto";
        return !value.Contains(':');
    }
}
