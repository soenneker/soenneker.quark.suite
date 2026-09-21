# SeoHead

`Soenneker.Quark.SeoHead` supplies page metadata through Blazor's `HeadOutlet`. It does not render a visible UI element or require Quark service registration.

```razor
@using Soenneker.Quark

<SeoHead Title="Background jobs | Example"
         Description="Schedule and monitor background work in your application."
         CanonicalUrl="https://example.com/jobs/"
         SiteName="Example"
         Locale="en_US"
         SocialImageUrl="https://example.com/images/jobs.png"
         SocialImageAlt="The background jobs dashboard"
         SocialImageWidth="1200"
         SocialImageHeight="630" />
```

Render one `SeoHead` per page. It owns the title, description, robots, Open Graph, and Twitter metadata, so remove competing `PageTitle` components and static copies of those tags from the host document.

The host must render `<HeadOutlet />` in its HTML head. For standalone Blazor WebAssembly, register `HeadOutlet` at `head::after` in `Program.cs`. WebAssembly metadata appears after the application renders; this component does not add prerendering or change HTTP status codes.

`Title` and `Description` are required editor parameters. Optional site, locale, image, canonical, and structured-data tags are omitted when not supplied. Image URLs and canonical URLs must be absolute HTTP or HTTPS URLs. The caller owns the production domain, canonical path, query-string policy, and trailing-slash convention.

`NoIndex="true"` emits `noindex, follow` and suppresses canonical, social, and structured-data tags. Use this for error pages or internal test pages. It is not an access-control mechanism.

`StructuredDataJson` accepts a serialized JSON object or array, validates the JSON, and escapes script delimiters. Keep schema types, breadcrumbs, repository links, and serialization in the application. For trimmed or AOT applications, use a source-generated JSON serializer or `Utf8JsonWriter` to produce that string.

The default Twitter card is `summary_large_image` when a social image is supplied, otherwise `summary`. `TwitterCard` and `OpenGraphType` allow explicit overrides.

## Search and sharing text

Search uses `Title` and `Description`. Open Graph uses `SocialTitle` and `SocialDescription`, falling back to the search text. Twitter uses its own `TwitterTitle` and `TwitterDescription`, falling back to the social text, then the search text. Blank overrides also fall back.

`TwitterImageUrl` overrides the shared image for Twitter. Supply `TwitterImageAlt` for that image; the shared alt text is only reused when the image is shared. `SocialImageType` adds the Open Graph MIME type. `TwitterSite` and `TwitterCreator` accept handles with the leading `@`.

```razor
<SeoHead Title="Lead routing software | Example"
         Description="Route incoming leads to the right team."
         SocialTitle="Get every lead to the right team"
         SocialDescription="See how Example simplifies lead routing."
         CanonicalUrl="https://example.com/lead-routing/"
         SocialImageUrl="https://example.com/images/routing.png"
         SocialImageAlt="Lead routing rules in Example"
         TwitterSite="@@example"
         TwitterCreator="@@author" />
```

The doubled `@@` above is Razor's escape for a literal `@` in markup.

## Robots controls

| Parameter | Default | Effect |
| --- | --- | --- |
| `NoIndex` | `false` | Emits `noindex`; suppresses managed canonical, alternate-language, social, and JSON-LD tags |
| `NoFollow` | `false` | Emits `nofollow` instead of `follow` |
| `NoSnippet` | `false` | Emits `nosnippet`; omits text and video preview limits |
| `NoImageIndex` | `false` | Emits `noimageindex` |
| `MaxSnippet` | `null` | Maximum text snippet length; `-1` allows any length, `0` disallows a text snippet |
| `MaxImagePreview` | `"large"` | `"none"`, `"standard"`, `"large"`, or `null` to omit |
| `MaxVideoPreview` | `null` | Maximum video preview duration in seconds; `-1` allows any length, `0` allows a still image |

Preview directives are omitted on noindex pages. Values below `-1` for snippet/video limits and nonpositive supplied image dimensions are rejected. These controls follow Google's [robots metadata specification](https://developers.google.com/search/docs/crawling-indexing/robots-meta-tag).

## Localized pages

```razor
<SeoHead Title="Example" Description="Localized product page"
         CanonicalUrl="https://example.com/en/"
         Locale="en_US"
         AlternateLocales="@OtherLocales"
         AlternateLanguageUrls="@Languages" />

@code {
    private static readonly string[] OtherLocales = ["fr_CA"];
    private static readonly IReadOnlyDictionary<string, string> Languages =
        new Dictionary<string, string>
        {
            ["en"] = "https://example.com/en/",
            ["fr-CA"] = "https://example.com/fr-ca/",
            ["x-default"] = "https://example.com/"
        };
}
```

Use language tags with hyphens for `hreflang` and Open Graph locales with underscores. Include the current language, and configure reciprocal alternatives on each localized page. Alternate URLs must be absolute; duplicate language keys that differ only by case are rejected. The component does not infer localization from routes.

## Articles

Set `OpenGraphType="article"` to emit `ArticlePublishedTime`, `ArticleModifiedTime`, `ArticleAuthorUrls`, `ArticleSection`, and `ArticleTags`. Dates use invariant ISO 8601 with their time-zone offsets. Author URLs and tags each produce repeatable Open Graph tags, following the [Open Graph article specification](https://ogp.me/#type_article).

```razor
<SeoHead Title="A guide to lead routing | Example"
         Description="Learn how to route incoming leads."
         CanonicalUrl="https://example.com/guides/lead-routing/"
         OpenGraphType="article"
         Author="Example Editorial"
         ArticlePublishedTime="@Published"
         ArticleAuthorUrls="@Authors"
         ArticleSection="Guides"
         ArticleTags="@Tags" />

@code {
    private static readonly DateTimeOffset Published =
        new(2026, 9, 20, 9, 0, 0, TimeSpan.Zero);
    private static readonly string[] Authors = ["https://example.com/authors/editorial/"];
    private static readonly string[] Tags = ["Lead routing", "Automation"];
}
```

`Author` independently supplies the HTML author meta tag. Structured article data still belongs in `StructuredDataJson`; Open Graph article tags do not create schema.org markup.

## Additional metadata

Child content extends the head for metadata not covered by the component:

```razor
<SeoHead Title="Example" Description="Example product page">
    <meta name="theme-color" content="#123456" />
</SeoHead>
```

Child content and the HTML author tag remain present on noindex pages. Do not duplicate tags already managed by `SeoHead`. Advanced Twitter player/app cards require their additional metadata through child content; setting `TwitterCard` alone does not supply those tags.

## Application responsibilities

The component has no hardcoded domains, repository URLs, brands, or language defaults. It does not generate sitemaps, set robots.txt rules, redirect aliases, authorize pages, return 404 responses, or add server rendering. Keep those concerns in the application and hosting configuration.

Render only one metadata owner at a time. Updating parameters replaces the previous head content, so images, article tags, and language alternatives do not remain after navigating to a page that omits them. If multiple routes reuse the same component instance, update its parameters when navigation changes.
