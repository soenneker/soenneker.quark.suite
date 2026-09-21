using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Supplies page titles, search metadata, social sharing metadata, and optional JSON-LD to a Blazor HeadOutlet.
/// Use one instance per page. The host must render HeadOutlet for the metadata to appear.
/// </summary>
public interface ISeoHead
{
    /// <summary>The page title, also used as the default title for Open Graph and Twitter cards.</summary>
    string Title { get; set; }

    /// <summary>The page description, also used as the default description for Open Graph and Twitter cards.</summary>
    string Description { get; set; }

    /// <summary>
    /// The absolute public HTTP or HTTPS canonical URL. Omit to suppress the canonical link and og:url.
    /// The application owns URL normalization, including query strings, aliases, and trailing slashes.
    /// </summary>
    string? CanonicalUrl { get; set; }

    /// <summary>The optional site name for Open Graph.</summary>
    string? SiteName { get; set; }

    /// <summary>The optional Open Graph locale, such as en_US. No locale is assumed by default.</summary>
    string? Locale { get; set; }

    /// <summary>Other Open Graph locales in which this content is available, such as fr_FR.</summary>
    IReadOnlyList<string>? AlternateLocales { get; set; }

    /// <summary>
    /// Maps language tags such as en, fr-CA, or x-default to absolute alternate page URLs.
    /// Include the current language and supply reciprocal links on the alternate pages.
    /// </summary>
    IReadOnlyDictionary<string, string>? AlternateLanguageUrls { get; set; }

    /// <summary>The Open Graph object type. Defaults to website.</summary>
    string OpenGraphType { get; set; }

    /// <summary>Social sharing title. Falls back to Title when omitted or blank.</summary>
    string? SocialTitle { get; set; }

    /// <summary>Social sharing description. Falls back to Description when omitted or blank.</summary>
    string? SocialDescription { get; set; }

    /// <summary>The optional absolute public HTTP or HTTPS image URL shared by Open Graph and Twitter cards.</summary>
    string? SocialImageUrl { get; set; }

    /// <summary>The accessible description of the social image. Omitted when no image is supplied.</summary>
    string? SocialImageAlt { get; set; }

    /// <summary>The optional MIME type of the Open Graph image, such as image/png.</summary>
    string? SocialImageType { get; set; }

    /// <summary>The social image width in pixels, when known. Must be positive if supplied.</summary>
    int? SocialImageWidth { get; set; }

    /// <summary>The social image height in pixels, when known. Must be positive if supplied.</summary>
    int? SocialImageHeight { get; set; }

    /// <summary>
    /// The optional Twitter card type. Defaults to summary_large_image when an image is supplied, otherwise summary.
    /// </summary>
    string? TwitterCard { get; set; }

    /// <summary>The website's Twitter/X handle, including the leading @.</summary>
    string? TwitterSite { get; set; }

    /// <summary>The content creator's Twitter/X handle, including the leading @.</summary>
    string? TwitterCreator { get; set; }

    /// <summary>Twitter card title. Falls back to SocialTitle, then Title.</summary>
    string? TwitterTitle { get; set; }

    /// <summary>Twitter card description. Falls back to SocialDescription, then Description.</summary>
    string? TwitterDescription { get; set; }

    /// <summary>An optional absolute Twitter card image URL. Falls back to SocialImageUrl.</summary>
    string? TwitterImageUrl { get; set; }

    /// <summary>
    /// The Twitter image description. Falls back to SocialImageAlt only when Twitter uses the shared social image.
    /// Supply this when overriding TwitterImageUrl with a different image.
    /// </summary>
    string? TwitterImageAlt { get; set; }

    /// <summary>
    /// Emits noindex and suppresses managed canonical, language-alternative, social, and structured data metadata when true.
    /// Link following is controlled separately by NoFollow. Author and custom child content are retained.
    /// This does not change the HTTP response status or prevent access to the page.
    /// </summary>
    bool NoIndex { get; set; }

    /// <summary>Emits nofollow instead of follow in robots metadata.</summary>
    bool NoFollow { get; set; }

    /// <summary>Emits nosnippet and takes precedence over MaxSnippet and MaxVideoPreview.</summary>
    bool NoSnippet { get; set; }

    /// <summary>Emits noimageindex to request that images on the page not be indexed.</summary>
    bool NoImageIndex { get; set; }

    /// <summary>Maximum search text snippet length. Use -1 for no limit or 0 for no text snippet. Omitted by default.</summary>
    int? MaxSnippet { get; set; }

    /// <summary>
    /// Maximum image preview size: none, standard, or large. Defaults to large; null omits the directive.
    /// Preview directives are omitted when NoIndex is true.
    /// </summary>
    string? MaxImagePreview { get; set; }

    /// <summary>Maximum search video preview length in seconds. Use -1 for no limit or 0 for a still image. Omitted by default.</summary>
    int? MaxVideoPreview { get; set; }

    /// <summary>Optional author name for the HTML author meta tag.</summary>
    string? Author { get; set; }

    /// <summary>Article publication time, emitted in ISO 8601 format only when OpenGraphType is article.</summary>
    DateTimeOffset? ArticlePublishedTime { get; set; }

    /// <summary>Article modification time, emitted in ISO 8601 format only when OpenGraphType is article.</summary>
    DateTimeOffset? ArticleModifiedTime { get; set; }

    /// <summary>Absolute public author profile URLs, emitted only when OpenGraphType is article.</summary>
    IReadOnlyList<string>? ArticleAuthorUrls { get; set; }

    /// <summary>Article section name, emitted only when OpenGraphType is article.</summary>
    string? ArticleSection { get; set; }

    /// <summary>Article topic tags, emitted only when OpenGraphType is article.</summary>
    IReadOnlyList<string>? ArticleTags { get; set; }

    /// <summary>
    /// Optional serialized JSON-LD supplied by the application. Must be a valid JSON object or array.
    /// Script delimiters are escaped before rendering. Schema types, URLs, and serialization belong to the caller.
    /// </summary>
    string? StructuredDataJson { get; set; }

    /// <summary>
    /// Additional head markup for metadata not managed by this component. Always rendered, including on noindex pages.
    /// Do not duplicate managed titles, descriptions, canonical links, or robots metadata here.
    /// </summary>
    RenderFragment? ChildContent { get; set; }
}
