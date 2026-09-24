using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
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
        var cut = Render<Image>(p => p.Add(c => c.Source, "/photo.avif").Add(c => c.AutoSrcSet, true));
        cut.Find("img").GetAttribute("srcset").Should().Be("/photo-480.avif 480w, /photo-960.avif 960w, /photo-1440.avif 1440w");
        cut.Find("img").GetAttribute("sizes").Should().Be("100vw");
        cut.Render(p => p.Add(c => c.SrcSet, "/custom.avif 2x"));
        cut.Find("img").GetAttribute("srcset").Should().Be("/custom.avif 2x");
        cut.Render(p => p.Add(c => c.AutoSrcSet, false).Add(c => c.SrcSet, (string?)null));
        cut.Find("img").HasAttribute("srcset").Should().BeFalse();
        cut.Find("img").HasAttribute("sizes").Should().BeFalse();
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
}
