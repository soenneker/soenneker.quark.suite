using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public sealed class CitationSourceInput
{
    public string Url { get; set; } = "";

    public RenderFragment? Title { get; set; }

    public RenderFragment? Description { get; set; }

    public static CitationSourceInput Create(string url, string? title = null, string? description = null)
    {
        return new CitationSourceInput
        {
            Url = url,
            Title = title is null ? null : builder => builder.AddContent(0, title),
            Description = description is null ? null : builder => builder.AddContent(0, description)
        };
    }
}

public sealed class ResolvedCitation
{
    public string Url { get; init; } = "";

    public RenderFragment? Title { get; init; }

    public RenderFragment? Description { get; init; }

    public string SiteName { get; init; } = "Source";

    public string FaviconSrc { get; init; } = "";
}

internal static class CitationSourceResolver
{
    public static ResolvedCitation Resolve(CitationSourceInput input)
    {
        var url = ParseUrl(input.Url);
        var host = url?.Host ?? "";

        return new ResolvedCitation
        {
            Url = url?.AbsoluteUri ?? "",
            Title = input.Title,
            Description = input.Description,
            SiteName = FormatSiteName(host),
            FaviconSrc = host.Length == 0 ? "" : $"https://www.google.com/s2/favicons?domain={Uri.EscapeDataString(host)}&sz=64"
        };
    }

    private static Uri? ParseUrl(string value)
    {
        var trimmed = value.Trim();
        if (trimmed.Length == 0)
            return null;

        if (!Uri.TryCreate(trimmed, UriKind.Absolute, out var uri))
            Uri.TryCreate($"https://{trimmed}", UriKind.Absolute, out uri);

        if (uri is null || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            return null;

        return uri;
    }

    private static string FormatSiteName(string host)
    {
        if (host.Length == 0)
            return "Source";

        var trimmed = host.StartsWith("www.", StringComparison.OrdinalIgnoreCase) ? host[4..] : host;
        var parts = trimmed.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var label = parts.Length > 1 ? parts[^2] : parts[0];

        if (label.Length <= 3)
            return label.ToUpperInvariant();

        return char.ToUpperInvariant(label[0]) + label[1..].ToLowerInvariant();
    }
}
