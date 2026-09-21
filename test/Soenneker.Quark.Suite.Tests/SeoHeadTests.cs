using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed class SeoHeadTests
{
    [Test]
    public void Minimal_metadata_does_not_invent_site_urls_or_images()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Search & discovery")
            .Add(c => c.Description, "A page about <metadata>."));

        head.Find("title").TextContent.Should().Be("Search & discovery");
        head.Find("meta[name='description']").GetAttribute("content").Should().Be("A page about <metadata>.");
        head.Find("meta[name='twitter:card']").GetAttribute("content").Should().Be("summary");
        head.FindAll("link[rel='canonical'], meta[property='og:url'], meta[property='og:site_name'], meta[property='og:locale'], meta[property='og:image'], script").Should().BeEmpty();
    }

    [Test]
    public void Page_updates_replace_head_metadata_and_preserve_the_supplied_canonical()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        var page = context.Render<SeoHead>(p => p
            .Add(c => c.Title, "First page")
            .Add(c => c.Description, "First description")
            .Add(c => c.CanonicalUrl, "https://example.com/first/")
            .Add(c => c.SiteName, "Example")
            .Add(c => c.Locale, "fr_FR")
            .Add(c => c.SocialImageUrl, "https://example.com/card.png")
            .Add(c => c.SocialImageAlt, "Preview")
            .Add(c => c.SocialImageWidth, 1200)
            .Add(c => c.SocialImageHeight, 630));

        head.Find("meta[name='twitter:card']").GetAttribute("content").Should().Be("summary_large_image");
        head.Find("meta[property='og:site_name']").GetAttribute("content").Should().Be("Example");
        head.Find("meta[property='og:locale']").GetAttribute("content").Should().Be("fr_FR");
        head.Find("meta[property='og:image:width']").GetAttribute("content").Should().Be("1200");
        head.Find("meta[property='og:image:height']").GetAttribute("content").Should().Be("630");
        head.Find("meta[name='twitter:image:alt']").GetAttribute("content").Should().Be("Preview");

        page.Render(p => p
            .Add(c => c.Title, "Second page")
            .Add(c => c.Description, "Second description")
            .Add(c => c.CanonicalUrl, "https://example.com/second/?language=fr")
            .Add(c => c.SocialImageUrl, (string?)null));

        head.FindAll("title").Should().ContainSingle().Which.TextContent.Should().Be("Second page");
        head.FindAll("meta[name='description']").Should().ContainSingle().Which.GetAttribute("content").Should().Be("Second description");
        head.FindAll("link[rel='canonical']").Should().ContainSingle().Which.GetAttribute("href").Should().Be("https://example.com/second/?language=fr");
        head.Find("meta[property='og:url']").GetAttribute("content").Should().Be("https://example.com/second/?language=fr");
        head.FindAll("meta[property^='og:image'], meta[name^='twitter:image']").Should().BeEmpty();
    }

    [Test]
    public void Noindex_removes_previous_public_metadata_and_can_restore_it()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        var page = context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Example")
            .Add(c => c.Description, "Description")
            .Add(c => c.CanonicalUrl, "https://example.com/")
            .Add(c => c.StructuredDataJson, "{\"name\":\"Example\"}"));

        page.Render(p => p.Add(c => c.NoIndex, true));

        head.Find("meta[name='robots']").GetAttribute("content").Should().Be("noindex, follow");
        head.FindAll("link[rel='canonical'], meta[property], meta[name^='twitter:'], script").Should().BeEmpty();
        head.Find("title").TextContent.Should().Be("Example");

        page.Render(p => p.Add(c => c.NoIndex, false));

        head.FindAll("link[rel='canonical']").Should().ContainSingle();
        head.FindAll("script[type='application/ld+json']").Should().ContainSingle();
    }

    [Test]
    public void Json_ld_cannot_close_its_script_and_is_removed_when_cleared()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        const string payload = "</script><script>alert('example')</script>";
        var page = context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Example")
            .Add(c => c.Description, "Description")
            .Add(c => c.StructuredDataJson, "{\"name\":\"" + payload + "\"}"));

        var script = head.FindAll("script").Should().ContainSingle().Which;
        script.GetAttribute("type").Should().Be("application/ld+json");
        script.TextContent.Should().NotContain("<");
        using var data = JsonDocument.Parse(script.TextContent);
        data.RootElement.GetProperty("name").GetString().Should().Be(payload);

        page.Render(p => p.Add(c => c.StructuredDataJson, (string?)null));
        head.FindAll("script").Should().BeEmpty();
    }

    [Test]
    [Arguments("not JSON", typeof(JsonException))]
    [Arguments("true", typeof(ArgumentException))]
    public void Invalid_structured_data_is_rejected(string json, Type exceptionType)
    {
        using var context = CreateContext();
        Action render = () => context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Example")
            .Add(c => c.Description, "Description")
            .Add(c => c.StructuredDataJson, json));

        exceptionType.IsInstanceOfType(render.Should().Throw<Exception>().Which).Should().BeTrue();
    }

    [Test]
    [Arguments("/relative")]
    [Arguments("javascript:alert(1)")]
    public void Canonical_urls_must_be_absolute_http_urls(string url)
    {
        using var context = CreateContext();
        Action render = () => context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Example")
            .Add(c => c.Description, "Description")
            .Add(c => c.CanonicalUrl, url));

        render.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Robots_controls_compose_without_contradictory_snippet_rules()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        var page = context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Example")
            .Add(c => c.Description, "Description")
            .Add(c => c.NoFollow, true)
            .Add(c => c.NoImageIndex, true)
            .Add(c => c.MaxSnippet, -1)
            .Add(c => c.MaxImagePreview, "standard")
            .Add(c => c.MaxVideoPreview, 10));

        head.Find("meta[name='robots']").GetAttribute("content").Should()
            .Be("index, nofollow, noimageindex, max-snippet:-1, max-image-preview:standard, max-video-preview:10");

        page.Render(p => p.Add(c => c.NoSnippet, true));
        head.Find("meta[name='robots']").GetAttribute("content").Should()
            .Be("index, nofollow, nosnippet, noimageindex, max-image-preview:standard");

        page.Render(p => p.Add(c => c.NoIndex, true));
        head.Find("meta[name='robots']").GetAttribute("content").Should()
            .Be("noindex, nofollow, nosnippet, noimageindex");
    }

    [Test]
    public void Language_alternates_are_removed_when_the_next_page_has_none()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        var page = context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Example")
            .Add(c => c.Description, "Description")
            .Add(c => c.AlternateLanguageUrls, new Dictionary<string, string>
            {
                ["en"] = "https://example.com/en/",
                ["fr-CA"] = "https://example.com/fr-ca/",
                ["x-default"] = "https://example.com/"
            })
            .Add(c => c.AlternateLocales, new[] { "fr_CA", "en_US" }));

        head.FindAll("link[rel='alternate']").Select(e => e.GetAttribute("hreflang")).Should()
            .BeEquivalentTo("en", "fr-CA", "x-default");
        head.FindAll("meta[property='og:locale:alternate']").Should().HaveCount(2);

        page.Render(p => p.Add(c => c.NoIndex, true));
        head.FindAll("link[rel='alternate'], meta[property='og:locale:alternate']").Should().BeEmpty();
        page.Render(p => p
            .Add(c => c.NoIndex, false)
            .Add(c => c.AlternateLanguageUrls, (IReadOnlyDictionary<string, string>?)null)
            .Add(c => c.AlternateLocales, (IReadOnlyList<string>?)null));
        head.FindAll("link[rel='alternate'], meta[property='og:locale:alternate']").Should().BeEmpty();
    }

    [Test]
    public void Twitter_overrides_do_not_change_search_or_open_graph_metadata()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        var page = context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Search title")
            .Add(c => c.Description, "Search description")
            .Add(c => c.SocialTitle, "Social title")
            .Add(c => c.SocialDescription, "Social description")
            .Add(c => c.SocialImageUrl, "https://example.com/social.png")
            .Add(c => c.SocialImageAlt, "Social preview")
            .Add(c => c.SocialImageType, "image/png")
            .Add(c => c.TwitterTitle, "Twitter title")
            .Add(c => c.TwitterDescription, "Twitter description")
            .Add(c => c.TwitterImageUrl, "https://example.com/twitter.png")
            .Add(c => c.TwitterSite, "@example")
            .Add(c => c.TwitterCreator, "@author"));

        head.Find("title").TextContent.Should().Be("Search title");
        head.Find("meta[name='description']").GetAttribute("content").Should().Be("Search description");
        head.Find("meta[property='og:title']").GetAttribute("content").Should().Be("Social title");
        head.Find("meta[property='og:description']").GetAttribute("content").Should().Be("Social description");
        head.Find("meta[property='og:image:type']").GetAttribute("content").Should().Be("image/png");
        head.Find("meta[name='twitter:title']").GetAttribute("content").Should().Be("Twitter title");
        head.Find("meta[name='twitter:description']").GetAttribute("content").Should().Be("Twitter description");
        head.Find("meta[name='twitter:image']").GetAttribute("content").Should().Be("https://example.com/twitter.png");
        head.Find("meta[name='twitter:site']").GetAttribute("content").Should().Be("@example");
        head.Find("meta[name='twitter:creator']").GetAttribute("content").Should().Be("@author");
        // A different image must not inherit an unrelated accessible description.
        head.FindAll("meta[name='twitter:image:alt']").Should().BeEmpty();

        page.Render(p => p
            .Add(c => c.TwitterTitle, (string?)null)
            .Add(c => c.TwitterDescription, (string?)null)
            .Add(c => c.TwitterImageUrl, (string?)null));
        head.Find("meta[name='twitter:title']").GetAttribute("content").Should().Be("Social title");
        head.Find("meta[name='twitter:description']").GetAttribute("content").Should().Be("Social description");
        head.Find("meta[name='twitter:image:alt']").GetAttribute("content").Should().Be("Social preview");
    }

    [Test]
    public void Article_metadata_uses_iso_dates_and_does_not_leak_to_website_pages()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        var published = new DateTimeOffset(2026, 9, 20, 9, 30, 0, TimeSpan.FromHours(-5));
        var page = context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Article")
            .Add(c => c.Description, "Description")
            .Add(c => c.OpenGraphType, "article")
            .Add(c => c.Author, "Example Author")
            .Add(c => c.ArticlePublishedTime, published)
            .Add(c => c.ArticleModifiedTime, published.AddHours(1))
            .Add(c => c.ArticleAuthorUrls, new[] { "https://example.com/authors/one", "https://example.com/authors/two" })
            .Add(c => c.ArticleSection, "Guides")
            .Add(c => c.ArticleTags, new[] { "Blazor", "SEO" }));

        head.Find("meta[name='author']").GetAttribute("content").Should().Be("Example Author");
        head.Find("meta[property='article:published_time']").GetAttribute("content").Should()
            .Be(published.ToString("O", CultureInfo.InvariantCulture));
        head.Find("meta[property='article:modified_time']").GetAttribute("content").Should()
            .Be(published.AddHours(1).ToString("O", CultureInfo.InvariantCulture));
        head.FindAll("meta[property='article:author']").Should().HaveCount(2);
        head.FindAll("meta[property='article:tag']").Should().HaveCount(2);
        head.Find("meta[property='article:section']").GetAttribute("content").Should().Be("Guides");

        page.Render(p => p.Add(c => c.OpenGraphType, "website"));
        head.FindAll("meta[property^='article:']").Should().BeEmpty();
    }

    [Test]
    public void Custom_head_markup_is_preserved_on_noindex_pages()
    {
        using var context = CreateContext();
        var head = context.Render<HeadOutlet>();
        context.Render<SeoHead>(p => p
            .Add(c => c.Title, "Internal")
            .Add(c => c.Description, "Description")
            .Add(c => c.NoIndex, true)
            .AddChildContent("<meta name='theme-color' content='#123456' />"));

        head.Find("meta[name='theme-color']").GetAttribute("content").Should().Be("#123456");
        head.Find("meta[name='robots']").GetAttribute("content").Should().Be("noindex, follow");
    }

    [Test]
    public void Invalid_preview_limits_and_alternate_urls_are_rejected()
    {
        using var context = CreateContext();
        Action invalidSnippet = () => context.Render<SeoHead>(p => p.Add(c => c.MaxSnippet, -2));
        invalidSnippet.Should().Throw<ArgumentOutOfRangeException>();

        Action invalidImage = () => context.Render<SeoHead>(p => p.Add(c => c.MaxImagePreview, "huge"));
        invalidImage.Should().Throw<ArgumentException>();

        Action invalidAlternate = () => context.Render<SeoHead>(p => p.Add(c => c.AlternateLanguageUrls,
            new Dictionary<string, string> { ["en"] = "/relative" }));
        invalidAlternate.Should().Throw<ArgumentException>();
    }

    private static BunitContext CreateContext()
    {
        var context = new BunitContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        return context;
    }
}
