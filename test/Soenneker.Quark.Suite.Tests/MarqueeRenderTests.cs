using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Marquee_renders_duplicated_tracks()
    {
        var cut = Render<Marquee>(parameters => parameters
            .Add(p => p.ChildContent, "<span>Acme</span>"));

        var root = cut.Find("[data-slot='marquee']");
        root.ClassList.Should().Contain("overflow-hidden");
        root.GetAttribute("data-pause-on-hover").Should().Be("true");
        root.GetAttribute("data-fade").Should().Be("true");
        root.GetAttribute("style").Should().Contain("mask-image:linear-gradient");
        cut.FindAll("[data-slot='marquee-track']").Should().HaveCount(2);
        cut.FindAll("[data-slot='marquee-track']")[1].GetAttribute("aria-hidden").Should().Be("true");
    }

    [Test]
    public void Marquee_applies_duration_and_gap_variables()
    {
        var cut = Render<Marquee>(parameters => parameters
            .Add(p => p.ScrollDuration, 24)
            .Add(p => p.ItemGap, "2rem")
            .Add(p => p.ChildContent, "<span>Northstar</span>"));

        var style = cut.Find("[data-slot='marquee']").GetAttribute("style");
        style.Should().Contain("--marquee-duration:24s");
        style.Should().Contain("--marquee-gap:2rem");
    }

    [Test]
    public void Marquee_can_reverse_and_disable_fade()
    {
        var cut = Render<Marquee>(parameters => parameters
            .Add(p => p.Reverse, true)
            .Add(p => p.ShowFade, false)
            .Add(p => p.PauseOnHover, false)
            .Add(p => p.ChildContent, "<span>Velocity</span>"));

        var root = cut.Find("[data-slot='marquee']");
        root.GetAttribute("data-reverse").Should().Be("true");
        root.GetAttribute("data-fade").Should().Be("false");
        root.GetAttribute("data-pause-on-hover").Should().Be("false");
        root.GetAttribute("style").Should().NotContain("mask-image");
        cut.Find("[data-slot='marquee-track']").GetAttribute("style").Should().Contain("animation-direction:reverse");
    }
}
