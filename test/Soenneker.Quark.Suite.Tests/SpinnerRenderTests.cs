using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Spinner_renders_monochrome_arc_and_accessible_status_by_default()
    {
        var cut = Render<Spinner>();

        var root = cut.Find("[data-slot='spinner']");
        root.GetAttribute("role").Should().Be("status");
        root.GetAttribute("aria-label").Should().Be("Loading");
        root.GetAttribute("data-decorative").Should().Be("false");

        root.ClassList.Should().NotContain("quark-spinner-multicolor");
        root.GetAttribute("style").Should().Contain("contain:strict");
        root.GetAttribute("style").Should().Contain("content-visibility:auto");
        root.GetAttribute("style").Should().Contain("--spinner-foreground:currentColor");
        root.GetAttribute("style").Should().Contain("--spinner-rotation-duration:2.118s");
        root.GetAttribute("style").Should().Contain("--spinner-arc-duration:1.8s");
        root.GetAttribute("style").Should().Contain("--spinner-cycle-duration:7.2s");
        root.GetAttribute("style").Should().Contain("--spinner-color-duration:7.2s");
        cut.FindAll("svg").Should().BeEmpty();
        cut.FindAll("style").Should().BeEmpty();
        cut.FindAll("link").Should().BeEmpty();
        cut.FindAll(".quark-spinner-clipper").Should().HaveCount(2);
        cut.FindAll(".quark-spinner-circle").Should().HaveCount(2);
    }

    [Test]
    public void Spinner_cycles_colors_only_when_a_palette_is_supplied()
    {
        var cut = Render<Spinner>(parameters => parameters
            .Add(p => p.ForegroundColors, SpinnerColorPalettes.Google));

        var root = cut.Find("[data-slot='spinner']");
        root.ClassList.Should().Contain("quark-spinner-multicolor");
        root.GetAttribute("style").Should().Contain("--spinner-color-1:#4285f4");
        root.GetAttribute("style").Should().Contain("--spinner-color-2:#db4437");
        root.GetAttribute("style").Should().Contain("--spinner-color-3:#f4b400");
        root.GetAttribute("style").Should().Contain("--spinner-color-4:#0f9d58");
    }

    [Test]
    public void Spinner_applies_track_color_geometry_and_independent_speed_controls()
    {
        var cut = Render<Spinner>(parameters => parameters
            .Add(p => p.ShowTrack, true)
            .Add(p => p.TrackColor, "#111827")
            .Add(p => p.TrackOpacity, 0.35)
            .Add(p => p.ForegroundColor, "#f9fafb")
            .Add(p => p.StrokeWidth, 6)
            .Add(p => p.TrackStrokeWidth, 2)
            .Add(p => p.MinimumArcLength, 8)
            .Add(p => p.MaximumArcLength, 60)
            .Add(p => p.ArcTravel, 80)
            .Add(p => p.Speed, 2)
            .Add(p => p.RotationDuration, 2)
            .Add(p => p.ArcDuration, 3));

        cut.FindAll("[data-slot='spinner-track']").Should().HaveCount(1);
        var root = cut.Find("[data-slot='spinner']");
        var style = root.GetAttribute("style");
        style.Should().Contain("--spinner-track-color:#111827");
        style.Should().Contain("--spinner-track-opacity:0.35");
        style.Should().Contain("--spinner-track-stroke-width:4cqi");
        style.Should().Contain("--spinner-foreground:#f9fafb");
        style.Should().Contain("--spinner-stroke-width:12cqi");
        style.Should().Contain("--spinner-rotation-duration:1s");
        style.Should().Contain("--spinner-arc-duration:1.5s");
        style.Should().Contain("--spinner-arc-travel:80");
        var motion = cut.Find("[data-slot='spinner-motion']");
        motion.ClassList.Should().Contain("quark-spinner-linear");
    }

    [Test]
    public void Spinner_supports_reverse_and_decorative_rendering()
    {
        var cut = Render<Spinner>(parameters => parameters
            .Add(p => p.Reverse, true)
            .Add(p => p.Decorative, true));

        var root = cut.Find("[data-slot='spinner']");
        root.GetAttribute("aria-hidden").Should().Be("true");
        root.HasAttribute("role").Should().BeFalse();
        root.HasAttribute("aria-label").Should().BeFalse();
        root.GetAttribute("data-reverse").Should().Be("true");
        cut.Find("[data-slot='spinner-motion']").ClassList.Should().Contain("quark-spinner-linear");
    }
}
