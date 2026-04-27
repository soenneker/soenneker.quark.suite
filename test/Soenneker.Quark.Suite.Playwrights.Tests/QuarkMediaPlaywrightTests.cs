using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkMediaPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkMediaPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Media_demos_preserve_native_attributes_and_event_callbacks()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, msg) =>
        {
            if (msg.Type == "error")
                consoleErrors.Add(msg.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}images",
            static p => p.GetByText("Default styling.").First,
            expectedTitle: "Images - Quark Suite");

        var imageProbe = await page.Locator("img[data-slot='image']").Nth(2).EvaluateAsync<ImageProbe>(
            @"element => ({
                tagName: element.tagName.toLowerCase(),
                alt: element.getAttribute('alt'),
                loading: element.getAttribute('loading'),
                complete: element.complete
            })");

        imageProbe.tagName.Should().Be("img");
        imageProbe.alt.Should().Be("Lazy loaded image");
        imageProbe.loading.Should().Be("lazy");

        await page.GetByAltText("Image with events").DispatchEventAsync("load");
        await Assertions.Expect(page.GetByText("Image loaded successfully!")).ToBeVisibleAsync();

        await page.GotoAndWaitForReady(
            $"{BaseUrl}videos",
            static p => p.GetByText("HTML5 video with poster and standard controls.").First,
            expectedTitle: "Video & Audio - Quark Suite");

        var videoProbe = await page.Locator("video[data-slot='video']").First.EvaluateAsync<VideoProbe>(
            @"element => ({
                controls: element.controls,
                poster: element.getAttribute('poster'),
                tagName: element.tagName.toLowerCase()
            })");

        videoProbe.tagName.Should().Be("video");
        videoProbe.controls.Should().BeTrue();
        videoProbe.poster.Should().Contain("title_anouncement.jpg");

        await page.Locator("video[data-slot='video']").Nth(2).DispatchEventAsync("play");
        await Assertions.Expect(page.GetByText("Video is playing")).ToBeVisibleAsync();
        await page.Locator("video[data-slot='video']").Nth(2).DispatchEventAsync("pause");
        await Assertions.Expect(page.GetByText("Video paused")).ToBeVisibleAsync();

        var audioProbe = await page.Locator("audio[data-slot='audio']").First.EvaluateAsync<AudioProbe>(
            @"element => ({
                controls: element.controls,
                src: element.getAttribute('src'),
                tagName: element.tagName.toLowerCase()
            })");

        audioProbe.tagName.Should().Be("audio");
        audioProbe.controls.Should().BeTrue();
        audioProbe.src.Should().Contain("SoundHelix-Song-1.mp3");

        await page.Locator("audio[data-slot='audio']").Nth(1).DispatchEventAsync("play");
        await Assertions.Expect(page.GetByText("Audio is playing")).ToBeVisibleAsync();
        await page.Locator("audio[data-slot='audio']").Nth(1).DispatchEventAsync("pause");
        await Assertions.Expect(page.GetByText("Audio paused")).ToBeVisibleAsync();

        await page.GotoAndWaitForReady(
            $"{BaseUrl}iframes",
            static p => p.GetByText("Source URL and title for accessibility.").First,
            expectedTitle: "IFrames - Quark Suite");

        var iframeProbe = await page.Locator("iframe[data-slot='iframe']").First.EvaluateAsync<IFrameProbe>(
            @"element => ({
                title: element.getAttribute('title'),
                src: element.getAttribute('src'),
                tagName: element.tagName.toLowerCase()
            })");

        iframeProbe.tagName.Should().Be("iframe");
        iframeProbe.title.Should().Be("Example Website");
        iframeProbe.src.Should().Contain("example.com");

        await page.GetByTitle("Dynamic Content").DispatchEventAsync("load");
        await Assertions.Expect(page.GetByText("IFrame loaded successfully!")).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class ImageProbe
    {
        public string? tagName { get; set; }
        public string? alt { get; set; }
        public string? loading { get; set; }
        public bool complete { get; set; }
    }

    private sealed class VideoProbe
    {
        public string? tagName { get; set; }
        public bool controls { get; set; }
        public string? poster { get; set; }
    }

    private sealed class AudioProbe
    {
        public string? tagName { get; set; }
        public bool controls { get; set; }
        public string? src { get; set; }
    }

    private sealed class IFrameProbe
    {
        public string? tagName { get; set; }
        public string? title { get; set; }
        public string? src { get; set; }
    }
}
