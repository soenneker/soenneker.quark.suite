using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Image_preserves_native_image_attributes_and_decorative_alt()
    {
        var cut = Render<Image>(parameters => parameters
            .Add(p => p.Source, "/photo.jpg")
            .Add(p => p.Alt, string.Empty)
            .Add(p => p.Fluid, true)
            .Add(p => p.Lazy, true)
            .Add(p => p.Decoding, "async")
            .Add(p => p.FetchPriority, "high")
            .Add(p => p.SrcSet, "/photo-400.jpg 400w")
            .Add(p => p.Sizes, "(max-width: 600px) 100vw, 600px")
            .Add(p => p.ReferrerPolicy, "no-referrer")
            .Add(p => p.CrossOrigin, "anonymous")
            .Add(p => p.UseMap, "#map")
            .Add(p => p.IsMap, true)
            .Add(p => p.LongDesc, "/photo-description")
            .Add(p => p.Class, "rounded-md"));

        var image = cut.Find("img[data-slot='image']");

        image.GetAttribute("src").Should().Be("/photo.jpg");
        image.GetAttribute("alt").Should().Be(string.Empty);
        image.GetAttribute("loading").Should().Be("lazy");
        image.GetAttribute("decoding").Should().Be("async");
        image.GetAttribute("fetchpriority").Should().Be("high");
        image.GetAttribute("srcset").Should().Be("/photo-400.jpg 400w");
        image.GetAttribute("sizes").Should().Be("(max-width: 600px) 100vw, 600px");
        image.GetAttribute("referrerpolicy").Should().Be("no-referrer");
        image.GetAttribute("crossorigin").Should().Be("anonymous");
        image.GetAttribute("usemap").Should().Be("#map");
        image.HasAttribute("ismap").Should().BeTrue();
        image.GetAttribute("longdesc").Should().Be("/photo-description");
        image.GetAttribute("class").Should().ContainAll("w-full", "max-w-full", "h-auto", "rounded-md");
    }

    [Test]
    public void Video_preserves_native_media_attributes_and_child_sources()
    {
        var cut = Render<Video>(parameters => parameters
            .Add(p => p.Source, "/movie.mp4")
            .Add(p => p.Poster, "/poster.jpg")
            .Add(p => p.Autoplay, true)
            .Add(p => p.Loop, true)
            .Add(p => p.Muted, true)
            .Add(p => p.Preload, "metadata")
            .Add(p => p.CrossOrigin, "anonymous")
            .Add(p => p.PlaysInline, true)
            .Add(p => p.DisableRemotePlayback, true)
            .Add(p => p.DisablePictureInPicture, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "source");
                builder.AddAttribute(1, "src", "/movie.webm");
                builder.AddAttribute(2, "type", "video/webm");
                builder.CloseElement();
                builder.AddContent(3, "Fallback");
            })));

        var video = cut.Find("video[data-slot='video']");

        video.GetAttribute("src").Should().Be("/movie.mp4");
        video.GetAttribute("poster").Should().Be("/poster.jpg");
        video.HasAttribute("controls").Should().BeTrue();
        video.HasAttribute("autoplay").Should().BeTrue();
        video.HasAttribute("loop").Should().BeTrue();
        video.HasAttribute("muted").Should().BeTrue();
        video.GetAttribute("preload").Should().Be("metadata");
        video.GetAttribute("crossorigin").Should().Be("anonymous");
        video.HasAttribute("playsinline").Should().BeTrue();
        video.HasAttribute("disableremoteplayback").Should().BeTrue();
        video.HasAttribute("disablepictureinpicture").Should().BeTrue();
        video.QuerySelector("source")!.GetAttribute("type").Should().Be("video/webm");
        video.TextContent.Should().Contain("Fallback");
    }

    [Test]
    public void Audio_preserves_native_audio_attributes()
    {
        var cut = Render<Audio>(parameters => parameters
            .Add(p => p.Source, "/song.mp3")
            .Add(p => p.Loop, true)
            .Add(p => p.Muted, true)
            .Add(p => p.Preload, "none")
            .Add(p => p.CrossOrigin, "use-credentials"));

        var audio = cut.Find("audio[data-slot='audio']");

        audio.GetAttribute("src").Should().Be("/song.mp3");
        audio.HasAttribute("controls").Should().BeTrue();
        audio.HasAttribute("loop").Should().BeTrue();
        audio.HasAttribute("muted").Should().BeTrue();
        audio.GetAttribute("preload").Should().Be("none");
        audio.GetAttribute("crossorigin").Should().Be("use-credentials");
    }

    [Test]
    public void IFrame_preserves_accessibility_security_and_loading_attributes()
    {
        var cut = Render<IFrame>(parameters => parameters
            .Add(p => p.Source, "https://example.test")
            .Add(p => p.Title, "Example content")
            .Add(p => p.Name, "preview")
            .Add(p => p.Sandbox, "allow-scripts")
            .Add(p => p.Allow, "fullscreen")
            .Add(p => p.Lazy, true)
            .Add(p => p.ReferrerPolicy, "strict-origin")
            .Add(p => p.AllowFullscreen, true)
            .Add(p => p.AllowPaymentRequest, true)
            .Add(p => p.SrcDoc, "<p>Hello</p>"));

        var iframe = cut.Find("iframe[data-slot='iframe']");

        iframe.GetAttribute("src").Should().Be("https://example.test");
        iframe.GetAttribute("title").Should().Be("Example content");
        iframe.GetAttribute("name").Should().Be("preview");
        iframe.GetAttribute("sandbox").Should().Be("allow-scripts");
        iframe.GetAttribute("allow").Should().Be("fullscreen");
        iframe.GetAttribute("loading").Should().Be("lazy");
        iframe.GetAttribute("referrerpolicy").Should().Be("strict-origin");
        iframe.HasAttribute("allowfullscreen").Should().BeTrue();
        iframe.HasAttribute("allowpaymentrequest").Should().BeTrue();
        iframe.GetAttribute("srcdoc").Should().Be("<p>Hello</p>");
    }

    [Test]
    public void Svg_preserves_native_svg_attributes_and_quark_media_classes()
    {
        var cut = Render<Svg>(parameters => parameters
            .Add(p => p.ViewBox, "0 0 32 32")
            .Add(p => p.NativeWidth, "32")
            .Add(p => p.NativeHeight, "32")
            .Add(p => p.PreserveAspectRatio, "xMidYMid meet")
            .Add(p => p.Focusable, false)
            .Add(p => p.BoxSize, Size.Is4)
            .Add(p => p.Stroke, Stroke.Token("current"))
            .Add(p => p.StrokeWidth, StrokeWidth.Is2)
            .Add(p => p.Fill, Fill.None)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "path");
                builder.AddAttribute(1, "d", "M4 16h24");
                builder.CloseElement();
            })));

        var svg = cut.Find("svg[data-slot='svg']");

        svg.GetAttribute("xmlns").Should().Be("http://www.w3.org/2000/svg");
        svg.GetAttribute("viewBox").Should().Be("0 0 32 32");
        svg.GetAttribute("width").Should().Be("32");
        svg.GetAttribute("height").Should().Be("32");
        svg.GetAttribute("preserveAspectRatio").Should().Be("xMidYMid meet");
        svg.GetAttribute("focusable").Should().Be("false");
        svg.GetAttribute("class").Should().ContainAll("size-4", "stroke-current", "stroke-2", "fill-none");
        svg.QuerySelector("path")!.GetAttribute("d").Should().Be("M4 16h24");
    }
}
