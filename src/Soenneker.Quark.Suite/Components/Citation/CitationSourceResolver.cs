using System;

namespace Soenneker.Quark;

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
                    chars[i] = char.ToUpperInvariant(state.trimmed[state.labelStart + i]);
            });
        }

        return string.Create(labelLength, (trimmed, labelStart), static (chars, state) =>
        {
            chars[0] = char.ToUpperInvariant(state.trimmed[state.labelStart]);

            for (var i = 1; i < chars.Length; i++)
                chars[i] = char.ToLowerInvariant(state.trimmed[state.labelStart + i]);
        });
    }
}
