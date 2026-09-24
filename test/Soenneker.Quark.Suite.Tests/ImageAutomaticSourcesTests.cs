using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Image_intrinsic_dimensions_update_and_clear_without_attribute_dictionaries()
    {
        var cut = Render<Image>(p => p.Add(c => c.Source, "/logo.svg")
            .Add(c => c.IntrinsicWidth, 28).Add(c => c.IntrinsicHeight, 28));
        cut.Find("img").GetAttribute("width").Should().Be("28");
        cut.Find("img").GetAttribute("height").Should().Be("28");
        cut.Render(p => p.Add(c => c.IntrinsicWidth, 112).Add(c => c.IntrinsicHeight, 112));
        cut.Find("img").GetAttribute("width").Should().Be("112");
        cut.Render(p => p.Add(c => c.IntrinsicWidth, (int?)null).Add(c => c.IntrinsicHeight, (int?)null));
        cut.Find("img").HasAttribute("width").Should().BeFalse();
        cut.Find("img").HasAttribute("height").Should().BeFalse();
    }

    [Test]
    public void Image_intrinsic_dimensions_apply_to_composed_preview()
    {
        var cut = Render<Image>(p => p.Add(c => c.Source, "/logo.svg")
            .Add(c => c.IntrinsicWidth, 28).Add(c => c.IntrinsicHeight, 28)
            .AddChildContent<ImagePreview>());
        cut.Find("img").GetAttribute("width").Should().Be("28");
        cut.Find("img").GetAttribute("height").Should().Be("28");
        cut.Find("[data-slot=image]").HasAttribute("width").Should().BeFalse();
        cut.Render(p => p.Add(c => c.IntrinsicWidth, 112));
        cut.Find("img").GetAttribute("width").Should().Be("112");
    }

    [Test]
    public async Task Image_automatic_sources_follow_theme_and_preserve_url_suffixes()
    {
        var cut = Render<Image>(p => p.Add(c => c.Source, "/img/photo.png?v=2#preview")
            .Add(c => c.Extension, "avif").Add(c => c.AutoSrcSet, true).Add(c => c.AutoDark, true)
            .Add(c => c.SrcSetWidths, new[] { 480, 960 }).Add(c => c.Sizes, "600px"));
        cut.Find("img").GetAttribute("srcset").Should().Be("/img/photo-480.avif?v=2#preview 480w, /img/photo-960.avif?v=2#preview 960w");
        cut.Find("img").GetAttribute("sizes").Should().Be("600px");
        IThemeInterop theme = Services.GetRequiredService<IThemeInterop>();
        await cut.InvokeAsync(async () => await theme.SetMode(ThemeMode.Dark));
        cut.Find("img").GetAttribute("src").Should().Be("/img/photo-dark.avif?v=2#preview");
        cut.Find("img").GetAttribute("srcset").Should().Be("/img/photo-dark-480.avif?v=2#preview 480w, /img/photo-dark-960.avif?v=2#preview 960w");
        await cut.InvokeAsync(async () => await theme.SetMode(ThemeMode.Light));
        cut.Find("img").GetAttribute("src").Should().Be("/img/photo.avif?v=2#preview");
        cut.Render(p => p.Add(c => c.Source, "other.jpg").Add(c => c.AutoDark, false));
        await cut.InvokeAsync(async () => await theme.SetMode(ThemeMode.Dark));
        cut.Find("img").GetAttribute("src").Should().Be("other.avif");
        cut.Find("img").GetAttribute("srcset").Should().Be("other-480.avif 480w, other-960.avif 960w");
    }

    [Test]
    public void Image_automatic_sources_use_defaults_and_preserve_explicit_srcset()
    {
        var cut = Render<Image>(p => p.Add(c => c.Source, "/photo.png").Add(c => c.AutoSrcSet, true));
        cut.Find("img").GetAttribute("srcset").Should().Be("/photo-480.avif 480w, /photo-960.avif 960w, /photo-1440.avif 1440w");
        cut.Find("img").GetAttribute("sizes").Should().Be("100vw");
        cut.Find("img").GetAttribute("src").Should().Be("/photo.avif");
        cut.Find("img").GetAttribute("loading").Should().Be("lazy");
        cut.Find("img").GetAttribute("decoding").Should().Be("async");
        cut.Render(p => p.Add(c => c.SrcSet, "/custom.avif 2x"));
        cut.Find("img").GetAttribute("srcset").Should().Be("/custom.avif 2x");
        cut.Render(p => p.Add(c => c.AutoSrcSet, false).Add(c => c.SrcSet, (string?)null));
        cut.Find("img").HasAttribute("srcset").Should().BeFalse();
        cut.Find("img").HasAttribute("sizes").Should().BeFalse();
        cut.Find("img").GetAttribute("src").Should().Be("/photo.png");
        cut.Find("img").HasAttribute("loading").Should().BeFalse();
        cut.Find("img").HasAttribute("decoding").Should().BeFalse();
    }

    [Test]
    [Arguments("data:image/png;base64,abc")]
    [Arguments("blob:https://example.com/photo.jpg")]
    [Arguments("https://example.com")]
    [Arguments("//example.com")]
    public void Image_automatic_sources_skip_non_file_urls(string source)
    {
        var cut = Render<Image>(p => p.Add(c => c.Source, source).Add(c => c.AutoSrcSet, true));
        cut.Find("img").GetAttribute("src").Should().Be(source);
        cut.Find("img").HasAttribute("srcset").Should().BeFalse();
    }
    [Test]
    public void Image_automatic_sources_update_when_widths_mutate_and_remove_duplicates()
    {
        int[] widths = [480, 480, 960];
        var cut = Render<Image>(p => p.Add(c => c.Source, "/photo.png")
            .Add(c => c.Extension, "png").Add(c => c.AutoSrcSet, true).Add(c => c.SrcSetWidths, widths));
        cut.Find("img").GetAttribute("srcset").Should().Be("/photo-480.png 480w, /photo-960.png 960w");
        widths[1] = 1440;
        cut.Render(p => p.Add(c => c.SrcSetWidths, widths));
        cut.Find("img").GetAttribute("srcset").Should().Be("/photo-480.png 480w, /photo-1440.png 1440w, /photo-960.png 960w");
        cut.Render(p => p.Add(c => c.Extension, "avif"));
        cut.Find("img").GetAttribute("srcset").Should().Be("/photo-480.avif 480w, /photo-1440.avif 1440w, /photo-960.avif 960w");
        cut.Render(p => p.Add(c => c.SrcSetWidths, System.Array.Empty<int>()));
        cut.Find("img").HasAttribute("srcset").Should().BeFalse();
        cut.Find("img").HasAttribute("sizes").Should().BeFalse();
    }

    [Test]
    public void Image_default_widths_are_shared_and_read_only()
    {
        var first = new Image();
        var second = new Image();
        first.SrcSetWidths.Should().BeSameAs(second.SrcSetWidths);
        ((System.Collections.Generic.IList<int>)first.SrcSetWidths).IsReadOnly.Should().BeTrue();
    }
    [Test]
    public void Image_automatic_defaults_respect_explicit_overrides()
    {
        var cut = Render<Image>(p => p.Add(c => c.Source, "/photo.jpg").Add(c => c.AutoSrcSet, true)
            .Add(c => c.Extension, "").Add(c => c.Loading, "eager").Add(c => c.Decoding, "sync")
            .Add(c => c.Sizes, "600px"));
        cut.Find("img").GetAttribute("src").Should().Be("/photo.jpg");
        cut.Find("img").GetAttribute("srcset").Should().Contain("/photo-480.jpg 480w");
        cut.Find("img").GetAttribute("loading").Should().Be("eager");
        cut.Find("img").GetAttribute("decoding").Should().Be("sync");
        cut.Find("img").GetAttribute("sizes").Should().Be("600px");
    }
}
