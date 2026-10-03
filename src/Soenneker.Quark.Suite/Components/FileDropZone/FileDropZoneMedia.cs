using System;
using System.IO;

namespace Soenneker.Quark;

internal static class FileDropZoneMedia
{
    internal static string ContentType(FileDropZoneFile file)
    {
        if (!string.IsNullOrWhiteSpace(file.ContentType) && file.ContentType != "application/octet-stream")
        {
            int separator = file.ContentType.IndexOf(';');
            return (separator < 0 ? file.ContentType : file.ContentType[..separator]).Trim().ToLowerInvariant();
        }

        ReadOnlySpan<char> extension = Path.GetExtension(file.Name.AsSpan());
        if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)) return "image/jpeg";
        if (extension.Equals(".png", StringComparison.OrdinalIgnoreCase)) return "image/png";
        if (extension.Equals(".gif", StringComparison.OrdinalIgnoreCase)) return "image/gif";
        if (extension.Equals(".webp", StringComparison.OrdinalIgnoreCase)) return "image/webp";
        if (extension.Equals(".avif", StringComparison.OrdinalIgnoreCase)) return "image/avif";
        if (extension.Equals(".svg", StringComparison.OrdinalIgnoreCase)) return "image/svg+xml";
        if (extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase) || extension.Equals(".m4v", StringComparison.OrdinalIgnoreCase)) return "video/mp4";
        if (extension.Equals(".webm", StringComparison.OrdinalIgnoreCase)) return "video/webm";
        if (extension.Equals(".ogv", StringComparison.OrdinalIgnoreCase)) return "video/ogg";
        if (extension.Equals(".mov", StringComparison.OrdinalIgnoreCase)) return "video/quicktime";
        if (extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase)) return "application/pdf";
        return "application/octet-stream";
    }

    internal static bool CanPreview(FileDropZoneFile file) => CanPreview(ContentType(file));

    internal static bool CanPreview(string type)
    {
        return type.StartsWith("image/", StringComparison.Ordinal) || type.StartsWith("video/", StringComparison.Ordinal) || type == "application/pdf";
    }

    internal static bool IsPreviewUrl(string? url) => !string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri) &&
        (!uri.IsAbsoluteUri || uri.Scheme is "https" or "http" or "blob");
}
