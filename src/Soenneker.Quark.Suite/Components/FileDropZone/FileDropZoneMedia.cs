using System;
using System.IO;

namespace Soenneker.Quark;

internal static class FileDropZoneMedia
{
    internal static string ContentType(FileDropZoneFile file)
    {
        if (!string.IsNullOrWhiteSpace(file.ContentType) && file.ContentType != "application/octet-stream")
            return file.ContentType.Split(';')[0].Trim().ToLowerInvariant();

        return Path.GetExtension(file.Name).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".avif" => "image/avif",
            ".svg" => "image/svg+xml",
            ".mp4" or ".m4v" => "video/mp4",
            ".webm" => "video/webm",
            ".ogv" => "video/ogg",
            ".mov" => "video/quicktime",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream"
        };
    }

    internal static bool CanPreview(FileDropZoneFile file)
    {
        var type = ContentType(file);
        return type.StartsWith("image/", StringComparison.Ordinal) || type.StartsWith("video/", StringComparison.Ordinal) || type == "application/pdf";
    }

    internal static bool IsPreviewUrl(string? url) => !string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri) &&
        (!uri.IsAbsoluteUri || uri.Scheme is "https" or "http" or "blob");
}
