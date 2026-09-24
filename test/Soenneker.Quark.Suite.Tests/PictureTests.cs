using System;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Picture_preserves_source_order_and_fallback_image()
    {
        var cut = Render<Picture>(p => p.AddChildContent<PictureSource>(s => s
            .Add(c => c.Source, "/mobile.avif").Add(c => c.Media, "(max-width: 639px)"))
            .AddChildContent<Image>(i => i.Add(c => c.Source, "/desktop.avif").Add(c => c.Alt, "Inbox")));
        var children = cut.Find("picture").Children;
        children.Length.Should().Be(2);
        children[0].TagName.Should().Be("SOURCE");
        children[1].TagName.Should().Be("IMG");
        children[1].GetAttribute("alt").Should().Be("Inbox");
    }

    [Test]
    public void PictureSource_generates_candidates_and_preserves_intrinsic_original()
    {
        int[] widths = [480, 960, 960, 1440, 1920];
        var cut = Render<PictureSource>(p => p.Add(c => c.Source, "/inbox-dark.png?v=1#view")
            .Add(c => c.AutoSrcSet, true).Add(c => c.SrcSetWidths, widths)
            .Add(c => c.IntrinsicWidth, 1535).Add(c => c.IntrinsicHeight, 822)
            .Add(c => c.Sizes, "76rem").Add(c => c.Type, "image/avif"));
        cut.Find("source").GetAttribute("srcset").Should().Be("/inbox-dark-480.avif?v=1#view 480w, /inbox-dark-960.avif?v=1#view 960w, /inbox-dark-1440.avif?v=1#view 1440w, /inbox-dark.avif?v=1#view 1535w");
        cut.Find("source").GetAttribute("sizes").Should().Be("76rem");
        cut.Find("source").GetAttribute("width").Should().Be("1535");
        cut.Find("source").GetAttribute("height").Should().Be("822");
        cut.Find("source").GetAttribute("type").Should().Be("image/avif");
        widths[0] = 320;
        cut.Render(p => p.Add(c => c.SrcSetWidths, widths));
        cut.Find("source").GetAttribute("srcset").Should().StartWith("/inbox-dark-320.avif");
        cut.Render(p => p.Add(c => c.SrcSet, "/explicit.avif 2x"));
        cut.Find("source").GetAttribute("srcset").Should().Be("/explicit.avif 2x");
    }

    [Test]
    public void PictureSource_keeps_native_theme_conditions_and_clears_removed_parameters()
    {
        var cut = Render<PictureSource>(p => p.Add(c => c.Source, "/mobile.avif")
            .Add(c => c.Theme, ThemeMode.Dark).Add(c => c.Media, "(max-width: 639px)"));
        cut.Find("source").GetAttribute("media").Should().Be("(max-width: 639px) and (prefers-color-scheme: dark)");
        cut.Find("source").GetAttribute("data-quark-media").Should().Be("(max-width: 639px)");
        cut.Find("source").GetAttribute("data-quark-theme").Should().Be("dark");
        cut.Render(p => p.Add(c => c.Theme, ThemeMode.Light));
        cut.Find("source").GetAttribute("media").Should().Be("(max-width: 639px) and (prefers-color-scheme: light)");
        cut.Render(p => p.Add(c => c.Theme, (ThemeMode?)null).Add(c => c.Media, (string?)null));
        cut.Find("source").HasAttribute("data-quark-theme").Should().BeFalse();
        cut.Find("source").HasAttribute("media").Should().BeFalse();
    }

    [Test]
    public void PictureSource_rejects_invalid_widths()
    {
        Action render = () => Render<PictureSource>(p => p.Add(c => c.Source, "/image.png")
            .Add(c => c.AutoSrcSet, true).Add(c => c.SrcSetWidths, new[] { 0 }));
        render.Should().Throw<ArgumentOutOfRangeException>();
    }
}
