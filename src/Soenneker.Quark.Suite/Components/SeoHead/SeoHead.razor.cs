using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class SeoHead
{
    [Parameter, EditorRequired] public string Title { get; set; } = "";
    [Parameter, EditorRequired] public string Description { get; set; } = "";
    [Parameter] public string? CanonicalUrl { get; set; }
    [Parameter] public string? SiteName { get; set; }
    [Parameter] public string? Locale { get; set; }
    [Parameter] public IReadOnlyList<string>? AlternateLocales { get; set; }
    [Parameter] public IReadOnlyDictionary<string, string>? AlternateLanguageUrls { get; set; }
    [Parameter] public string OpenGraphType { get; set; } = "website";
    [Parameter] public string? SocialTitle { get; set; }
    [Parameter] public string? SocialDescription { get; set; }
    [Parameter] public string? SocialImageUrl { get; set; }
    [Parameter] public string? SocialImageAlt { get; set; }
    [Parameter] public string? SocialImageType { get; set; }
    [Parameter] public int? SocialImageWidth { get; set; }
    [Parameter] public int? SocialImageHeight { get; set; }
    [Parameter] public string? TwitterCard { get; set; }
    [Parameter] public string? TwitterSite { get; set; }
    [Parameter] public string? TwitterCreator { get; set; }
    [Parameter] public string? TwitterTitle { get; set; }
    [Parameter] public string? TwitterDescription { get; set; }
    [Parameter] public string? TwitterImageUrl { get; set; }
    [Parameter] public string? TwitterImageAlt { get; set; }
    [Parameter] public bool NoIndex { get; set; }
    [Parameter] public bool NoFollow { get; set; }
    [Parameter] public bool NoSnippet { get; set; }
    [Parameter] public bool NoImageIndex { get; set; }
    [Parameter] public int? MaxSnippet { get; set; }
    [Parameter] public string? MaxImagePreview { get; set; } = "large";
    [Parameter] public int? MaxVideoPreview { get; set; }
    [Parameter] public string? Author { get; set; }
    [Parameter] public DateTimeOffset? ArticlePublishedTime { get; set; }
    [Parameter] public DateTimeOffset? ArticleModifiedTime { get; set; }
    [Parameter] public IReadOnlyList<string>? ArticleAuthorUrls { get; set; }
    [Parameter] public string? ArticleSection { get; set; }
    [Parameter] public IReadOnlyList<string>? ArticleTags { get; set; }
    [Parameter] public string? StructuredDataJson { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string? _structuredDataJson;
    private string? _structuredDataMarkup;

    private string EffectiveSocialTitle => ValueOr(SocialTitle, Title);
    private string EffectiveSocialDescription => ValueOr(SocialDescription, Description);
    private string? EffectiveTwitterImageUrl => ValueOr(TwitterImageUrl, SocialImageUrl);
    private string? EffectiveTwitterImageAlt => ValueOr(TwitterImageAlt,
        !HasValue(TwitterImageUrl) || TwitterImageUrl == SocialImageUrl ? SocialImageAlt : null);
    private string EffectiveTwitterCard => ValueOr(TwitterCard, HasValue(EffectiveTwitterImageUrl) ? "summary_large_image" : "summary");

    private string RobotsContent
    {
        get
        {
            var directives = new List<string> { NoIndex ? "noindex" : "index", NoFollow ? "nofollow" : "follow" };
            if (NoSnippet)
                directives.Add("nosnippet");
            if (NoImageIndex)
                directives.Add("noimageindex");
            if (!NoIndex)
            {
                if (!NoSnippet && MaxSnippet.HasValue)
                    directives.Add("max-snippet:" + MaxSnippet.Value.ToString(CultureInfo.InvariantCulture));
                if (HasValue(MaxImagePreview))
                    directives.Add("max-image-preview:" + MaxImagePreview);
                if (!NoSnippet && MaxVideoPreview.HasValue)
                    directives.Add("max-video-preview:" + MaxVideoPreview.Value.ToString(CultureInfo.InvariantCulture));
            }
            return string.Join(", ", directives);
        }
    }

    protected override void OnParametersSet()
    {
        if (NoIndex)
            return;

        ValidateUrl(CanonicalUrl, nameof(CanonicalUrl));
        ValidateUrl(SocialImageUrl, nameof(SocialImageUrl));
        ValidateUrl(TwitterImageUrl, nameof(TwitterImageUrl));
        if (SocialImageWidth is <= 0)
            throw new ArgumentOutOfRangeException(nameof(SocialImageWidth), "Image width must be positive.");
        if (SocialImageHeight is <= 0)
            throw new ArgumentOutOfRangeException(nameof(SocialImageHeight), "Image height must be positive.");
        if (MaxSnippet is < -1)
            throw new ArgumentOutOfRangeException(nameof(MaxSnippet), "Snippet length must be -1 or greater.");
        if (MaxVideoPreview is < -1)
            throw new ArgumentOutOfRangeException(nameof(MaxVideoPreview), "Video preview length must be -1 or greater.");
        if (HasValue(MaxImagePreview) && MaxImagePreview is not ("none" or "standard" or "large"))
            throw new ArgumentException("Image preview must be none, standard, large, or null.", nameof(MaxImagePreview));

        if (AlternateLanguageUrls is not null)
        {
            var languages = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, string> alternate in AlternateLanguageUrls)
            {
                if (!HasValue(alternate.Key) || !languages.Add(alternate.Key))
                    throw new ArgumentException("Alternate language tags must be nonempty and unique.", nameof(AlternateLanguageUrls));
                ValidateRequiredUrl(alternate.Value, nameof(AlternateLanguageUrls));
            }
        }
        if (OpenGraphType == "article" && ArticleAuthorUrls is not null)
        {
            foreach (string url in ArticleAuthorUrls)
                ValidateRequiredUrl(url, nameof(ArticleAuthorUrls));
        }

        if (StructuredDataJson == _structuredDataJson)
            return;

        string? markup = null;
        if (HasValue(StructuredDataJson))
        {
            using JsonDocument document = JsonDocument.Parse(StructuredDataJson!);
            if (document.RootElement.ValueKind is not (JsonValueKind.Object or JsonValueKind.Array))
                throw new ArgumentException("JSON-LD must be a JSON object or array.", nameof(StructuredDataJson));

            // Preserve JSON values while preventing a literal closing tag from escaping the script element.
            markup = document.RootElement.GetRawText().Replace("<", "\\u003c", StringComparison.Ordinal);
        }

        _structuredDataJson = StructuredDataJson;
        _structuredDataMarkup = markup;
    }

    private static bool HasValue([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string? value) => !string.IsNullOrWhiteSpace(value);

    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(fallback))]
    private static string? ValueOr(string? value, string? fallback) => HasValue(value) ? value : fallback;

    private static string FormatDate(DateTimeOffset value) => value.ToString("O", CultureInfo.InvariantCulture);

    private static void ValidateRequiredUrl(string? value, string parameterName)
    {
        if (!HasValue(value))
            throw new ArgumentException("Supply an absolute public HTTP or HTTPS URL.", parameterName);
        ValidateUrl(value, parameterName);
    }

    private static void ValidateUrl(string? value, string parameterName)
    {
        if (HasValue(value) &&
            (!Uri.TryCreate(value, UriKind.Absolute, out Uri? uri) ||
             (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)))
            throw new ArgumentException("Supply an absolute public HTTP or HTTPS URL.", parameterName);
    }
}
