using System;
using System.Collections.Generic;
using System.Globalization;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

internal static class ImageSources
{
    internal static readonly IReadOnlyList<int> DefaultWidths = Array.AsReadOnly(new[] { 480, 960, 1440 });

    internal static string? BuildSrcSet(string? source, IReadOnlyList<int> widths, int? intrinsicWidth = null)
    {
        int extension = ImageExtensionIndex(source);
        if (extension < 0)
            return null;
        if (intrinsicWidth <= 0)
            throw new ArgumentOutOfRangeException(nameof(intrinsicWidth));
        var builder = new PooledStringBuilder(stackalloc char[256]);
        Span<char> digits = stackalloc char[10];
        // Most images use three widths. Avoid allocating a set for small lists.
        HashSet<int>? seen = widths.Count > 16 ? new HashSet<int>(widths.Count) : null;
        try
        {
            for (var i = 0; i < widths.Count; i++)
            {
                int width = widths[i];
                if (width <= 0)
                    throw new ArgumentOutOfRangeException(nameof(widths), "Image widths must be positive.");
                if (intrinsicWidth.HasValue && width >= intrinsicWidth.Value)
                    continue;
                if (seen is not null ? !seen.Add(width) : ContainsBefore(widths, i, width))
                    continue;

                width.TryFormat(digits, out int length, provider: CultureInfo.InvariantCulture);
                if (builder.Length > 0) builder.Append(", ");
                builder.Append(source.AsSpan(0, extension));
                builder.Append('-');
                builder.Append(digits[..length]);
                builder.Append(source.AsSpan(extension));
                builder.Append(' ');
                builder.Append(digits[..length]);
                builder.Append('w');
            }
            if (intrinsicWidth.HasValue)
            {
                intrinsicWidth.Value.TryFormat(digits, out int length, provider: CultureInfo.InvariantCulture);
                if (builder.Length > 0) builder.Append(", ");
                builder.Append(source);
                builder.Append(' ');
                builder.Append(digits[..length]);
                builder.Append('w');
            }
            return builder.ToString();
        }
        finally
        {
            builder.Dispose();
        }
    }

    private static bool ContainsBefore(IReadOnlyList<int> widths, int end, int width)
    {
        for (var i = 0; i < end; i++)
            if (widths[i] == width) return true;
        return false;
    }

    internal static int ImageExtensionIndex(string? source)
    {
        if (string.IsNullOrWhiteSpace(source) || source.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            source.StartsWith("blob:", StringComparison.OrdinalIgnoreCase))
            return -1;
        int suffix = source.AsSpan().IndexOfAny('?', '#');
        ReadOnlySpan<char> path = suffix < 0 ? source.AsSpan() : source.AsSpan(0, suffix);
        int extension = path.LastIndexOf('.');
        int authority = path.IndexOf("://", StringComparison.Ordinal);
        int fileStart = path.LastIndexOf('/') + 1;
        if (authority >= 0 && path[(authority + 3)..].IndexOf('/') < 0 ||
            path.StartsWith("//", StringComparison.Ordinal) && path[2..].IndexOf('/') < 0)
            return -1;
        return extension > fileStart ? extension : -1;
    }

    internal static string? AddImageSuffix(string? source, string suffix, bool skipExisting = false)
    {
        int extension = ImageExtensionIndex(source);
        if (extension < 0 || skipExisting && source.AsSpan(0, extension).EndsWith(suffix, StringComparison.Ordinal))
            return source;
        return source!.Insert(extension, suffix);
    }

    internal static string ApplyExtension(string source, string? extension)
    {
        if (string.IsNullOrWhiteSpace(extension) ||
            source.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            source.StartsWith("blob:", StringComparison.OrdinalIgnoreCase))
            return source;

        ReadOnlySpan<char> normalizedExtension = extension.AsSpan().Trim().TrimStart('.');
        if (normalizedExtension.Length == 0)
            return source;

        if (normalizedExtension.IndexOfAny("/\\?#:") >= 0)
            throw new ArgumentException("Extension must be a file extension, such as webp or .png.", nameof(extension));

        int suffixStart = source.AsSpan().IndexOfAny('?', '#');
        if (suffixStart < 0)
            suffixStart = source.Length;

        ReadOnlySpan<char> path = source.AsSpan(0, suffixStart);
        int authorityStart = path.StartsWith("//", StringComparison.Ordinal) ? 2 : path.IndexOf("://", StringComparison.Ordinal);
        if (authorityStart >= 0)
        {
            if (!path.StartsWith("//", StringComparison.Ordinal))
                authorityStart += 3;
            if (path[authorityStart..].IndexOf('/') < 0)
                return source;
        }

        int fileStart = path.LastIndexOf('/') + 1;
        if (fileStart == path.Length)
            return source;

        int dot = path.LastIndexOf('.');
        int extensionStart = dot > fileStart ? dot : path.Length;
        if (dot > fileStart && path[(dot + 1)..].SequenceEqual(normalizedExtension))
            return source;
        return string.Concat(path[..extensionStart], ".", normalizedExtension, source.AsSpan(suffixStart));
    }

}
