using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Audio_controls_mute_and_restore_the_previous_volume(CancellationToken cancellationToken)
    {
        double? requestedVolume = null;
        var cut = Render<AudioControls>(p => p.Add(c => c.Volume, 0.7)
            .Add(c => c.VolumeChanged, value => requestedVolume = value));

        cut.Find("[data-slot='audio-controls']").ClassList.Should().Contain("w-full");
        await cut.Find("button[aria-label='Mute']").ClickAsync();
        requestedVolume.Should().Be(0);
        cut.Render(p => p.Add(c => c.Volume, 0));
        await cut.Find("button[aria-label='Unmute']").ClickAsync();
        requestedVolume.Should().Be(0.7);
    }

    [Test]
    public async Task Audio_controls_delegate_playback_and_follow_external_state(CancellationToken cancellationToken)
    {
        var requests = 0;
        var cut = Render<AudioControls>(p => p.Add(c => c.Compact, true)
            .Add(c => c.StopInsteadOfPause, true).Add(c => c.OnTogglePlayback, () => requests++));

        await cut.Find("button").ClickAsync();
        requests.Should().Be(1);
        cut.Find("button").GetAttribute("aria-label").Should().Be("Play");
        cut.Find("button svg path").GetAttribute("d").Should().Be("M8 5v14l11-7z");
        cut.Render(p => p.Add(c => c.IsPlaying, true));
        cut.Find("button").GetAttribute("aria-label").Should().Be("Stop");
        cut.Find("button svg path").GetAttribute("d").Should().Be("M6 6h12v12H6z");
        cut.FindComponents<Slider>().Should().BeEmpty();
        cut.FindAll("audio").Should().BeEmpty();
    }

    [Test]
    public async Task Audio_controls_forward_seek_and_normalized_volume_and_handle_unknown_duration(CancellationToken cancellationToken)
    {
        double? seek = null;
        double? volume = null;
        var cut = Render<AudioControls>(p => p.Add(c => c.MediaDuration, 3661)
            .Add(c => c.CurrentTime, 65).Add(c => c.Volume, 0.25)
            .Add(c => c.CurrentTimeChanged, value => seek = value)
            .Add(c => c.VolumeChanged, value => volume = value));

        cut.Markup.Should().Contain("1:05 / 1:01:01");
        var sliders = cut.FindComponents<Slider>();
        var volumeSlider = sliders.Single(s => s.Instance.Label == "Volume");
        var positionSlider = sliders.Single(s => s.Instance.Label == "Playback position");
        await cut.InvokeAsync(() => volumeSlider.Instance.SliderValueChanged.InvokeAsync(0.7));
        await cut.InvokeAsync(() => positionSlider.Instance.SliderValueChanged.InvokeAsync(120));
        volume.Should().Be(0.7);
        seek.Should().Be(120);

        cut.Render(p => p.Add(c => c.MediaDuration, double.NaN));
        positionSlider.Instance.Disabled.Should().BeTrue();
        positionSlider.Instance.Max.Should().Be(1);
        cut.Render(p => p.Add(c => c.Disabled, true));
        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
        volumeSlider.Instance.Disabled.Should().BeTrue();
    }
}
