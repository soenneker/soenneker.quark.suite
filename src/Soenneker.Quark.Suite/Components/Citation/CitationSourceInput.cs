using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents the citation source input.
/// </summary>
public sealed class CitationSourceInput
{
    /// <summary>
    /// Gets or sets url.
    /// </summary>
    public string Url { get; set; } = "";

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    public RenderFragment? Title { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public RenderFragment? Description { get; set; }

    /// <summary>
    /// Executes the create operation.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="title">The title.</param>
    /// <param name="description">The description.</param>
    /// <returns>The result of the operation.</returns>
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

/// <summary>
/// Represents the resolved citation.
/// </summary>
public sealed class ResolvedCitation
{
    /// <summary>
    /// Gets or sets url.
    /// </summary>
    public string Url { get; init; } = "";

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    public RenderFragment? Title { get; init; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public RenderFragment? Description { get; init; }

    /// <summary>
    /// Gets or sets site name.
    /// </summary>
    public string SiteName { get; init; } = "Source";

    /// <summary>
    /// Gets or sets favicon src.
    /// </summary>
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
        var labelStart = 0;
        var labelEnd = trimmed.Length;
        var lastDot = trimmed.LastIndexOf('.');

        if (lastDot > 0)
        {
            labelEnd = lastDot;
            var previousDot = trimmed.LastIndexOf('.', lastDot - 1);

            if (previousDot >= 0)
                labelStart = previousDot + 1;
        }

        var labelLength = labelEnd - labelStart;

        if (labelLength <= 3)
        {
            return string.Create(labelLength, (trimmed, labelStart), static (chars, state) =>
            {
                for (var i = 0; i < chars.Length; i++)
                {
                    chars[i] = char.ToUpperInvariant(state.trimmed[state.labelStart + i]);
                }
            });
        }

        return string.Create(labelLength, (trimmed, labelStart), static (chars, state) =>
        {
            chars[0] = char.ToUpperInvariant(state.trimmed[state.labelStart]);

            for (var i = 1; i < chars.Length; i++)
            {
                chars[i] = char.ToLowerInvariant(state.trimmed[state.labelStart + i]);
            }
        });
    }
}
