using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Soenneker.Quark;

internal static class ImageSources
{
    internal static readonly IReadOnlyList<int> DefaultWidths = Array.AsReadOnly(new[] { 480, 960, 1440 });

    internal static string? BuildSrcSet(string? source, IReadOnlyList<int> widths, int? intrinsicWidth = null)
    {
        if (ImageExtensionIndex(source) < 0)
            return null;
        if (intrinsicWidth <= 0)
            throw new ArgumentOutOfRangeException(nameof(intrinsicWidth));
        var builder = new StringBuilder();
        var seen = new HashSet<int>();
        foreach (int width in widths)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(widths), "Image widths must be positive.");
            if (!seen.Add(width) || intrinsicWidth.HasValue && width >= intrinsicWidth.Value)
                continue;
            Append(AddImageSuffix(source, "-" + width.ToString(CultureInfo.InvariantCulture)), width);
        }
        if (intrinsicWidth.HasValue)
            Append(source, intrinsicWidth.Value);
        return builder.ToString();

        void Append(string? url, int width)
        {
            if (builder.Length > 0) builder.Append(", ");
            builder.Append(url).Append(' ').Append(width.ToString(CultureInfo.InvariantCulture)).Append('w');
        }
    }

    internal static int ImageExtensionIndex(string? source)
    {
        if (string.IsNullOrWhiteSpace(source) || source.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            source.StartsWith("blob:", StringComparison.OrdinalIgnoreCase))
            return -1;
        int suffix = source.IndexOfAny(['?', '#']);
        string path = suffix < 0 ? source : source[..suffix];
        int extension = path.LastIndexOf('.');
        int authority = path.IndexOf("://", StringComparison.Ordinal);
        int fileStart = path.LastIndexOf('/') + 1;
        if (authority >= 0 && path.IndexOf('/', authority + 3) < 0 ||
            path.StartsWith("//", StringComparison.Ordinal) && path.IndexOf('/', 2) < 0)
            return -1;
        return extension > fileStart ? extension : -1;
    }

    internal static string? AddImageSuffix(string? source, string suffix, bool skipExisting = false)
    {
        int extension = ImageExtensionIndex(source);
        if (extension < 0 || skipExisting && source![..extension].EndsWith(suffix, StringComparison.Ordinal))
            return source;
        return source!.Insert(extension, suffix);
    }

    internal static string ApplyExtension(string source, string? extension)
    {
        if (string.IsNullOrWhiteSpace(extension) ||
            source.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            source.StartsWith("blob:", StringComparison.OrdinalIgnoreCase))
            return source;

        extension = extension.Trim().TrimStart('.');
        if (extension.Length == 0)
            return source;

        if (extension.IndexOfAny(['/', '\\', '?', '#', ':']) >= 0)
            throw new ArgumentException("Extension must be a file extension, such as webp or .png.", nameof(extension));

        int suffixStart = source.IndexOfAny(['?', '#']);
        if (suffixStart < 0)
            suffixStart = source.Length;

        string path = source[..suffixStart];
        int authorityStart = path.StartsWith("//", StringComparison.Ordinal) ? 2 : path.IndexOf("://", StringComparison.Ordinal);
        if (authorityStart >= 0)
        {
            if (!path.StartsWith("//", StringComparison.Ordinal))
                authorityStart += 3;
            if (path.IndexOf('/', authorityStart) < 0)
                return source;
        }

        int fileStart = path.LastIndexOf('/') + 1;
        if (fileStart == path.Length)
            return source;

        int dot = path.LastIndexOf('.');
        int extensionStart = dot > fileStart ? dot : path.Length;
        return string.Concat(path.AsSpan(0, extensionStart), ".", extension, source.AsSpan(suffixStart));
    }

}
