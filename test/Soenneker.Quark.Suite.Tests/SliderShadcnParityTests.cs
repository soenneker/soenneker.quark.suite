using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Slider_matches_shadcn_base_classes()
    {
        var slider = Render<Slider>(parameters => parameters
            .Add(p => p.SliderValue, 75d)
            .Add(p => p.AriaLabel, "Volume"));

        string sliderClasses = slider.Find("[data-slot='slider']").GetAttribute("class")!;
        string sliderTrackClasses = slider.Find("[data-slot='slider-track']").GetAttribute("class")!;
        string sliderRangeClasses = slider.Find("[data-slot='slider-range']").GetAttribute("class")!;
        string sliderThumbClasses = slider.Find("[data-slot='slider-thumb']").GetAttribute("class")!;

        sliderClasses.Should().Contain("relative");
        sliderClasses.Should().Contain("flex");
        sliderClasses.Should().Contain("touch-none");
        sliderClasses.Should().Contain("data-disabled:opacity-50");
        sliderClasses.Should().NotContain("q-slider");
        slider.Find("[data-slot='slider']").GetAttribute("aria-label")!.Should().Be("Volume");

        sliderTrackClasses.Should().Contain("relative");
        sliderTrackClasses.Should().Contain("grow");
        sliderTrackClasses.Should().Contain("rounded-full");
        sliderTrackClasses.Should().Contain("bg-muted");
        sliderTrackClasses.Should().Contain("data-horizontal:h-1");

        sliderRangeClasses.Should().Contain("absolute");
        sliderRangeClasses.Should().Contain("bg-primary");
        sliderRangeClasses.Should().Contain("select-none");

        sliderThumbClasses.Should().Contain("relative");
        sliderThumbClasses.Should().Contain("block");
        sliderThumbClasses.Should().Contain("size-3");
        sliderThumbClasses.Should().Contain("rounded-full");
        sliderThumbClasses.Should().Contain("border");
        sliderThumbClasses.Should().Contain("border-ring");
        sliderThumbClasses.Should().Contain("bg-white");
        sliderThumbClasses.Should().Contain("focus-visible:ring-3");
        sliderThumbClasses.Should().NotContain("q-slider");
        slider.Find("[data-slot='slider-thumb']").HasAttribute("aria-label").Should().BeFalse();
    }

    [Fact]
    public void Slider_thumb_is_wrapped_in_absolute_positioning_container()
    {
        var slider = Render<Slider>(parameters => parameters
            .Add(p => p.Values, new[] { 200d, 800d })
            .Add(p => p.Min, 0d)
            .Add(p => p.Max, 1000d)
            .Add(p => p.AriaLabel, "Price Range"));

        var thumb = slider.Find("[data-slot='slider-thumb']");
        var wrapper = thumb.ParentElement;

        wrapper.Should().NotBeNull();
        wrapper!.TagName.Should().Be("SPAN");
        wrapper.GetAttribute("style").Should().Contain("position: absolute");
        wrapper.GetAttribute("style").Should().Contain("left: calc(20.00% + 3.6px)");
        wrapper.GetAttribute("style").Should().Contain("transform: var(--radix-slider-thumb-transform)");
        thumb.GetAttribute("style").Should().BeNull();
    }

    [Fact]
    public void Range_slider_uses_minimum_and_maximum_thumb_labels()
    {
        var slider = Render<Slider>(parameters => parameters
            .Add(p => p.Values, new[] { 200d, 800d })
            .Add(p => p.Min, 0d)
            .Add(p => p.Max, 1000d)
            .Add(p => p.AriaLabel, "Price Range"));

        var thumbs = slider.FindAll("[data-slot='slider-thumb']");

        thumbs.Should().HaveCount(2);
        thumbs[0].GetAttribute("aria-label")!.Should().Be("Minimum");
        thumbs[1].GetAttribute("aria-label")!.Should().Be("Maximum");
        slider.Find("[data-slot='slider']").GetAttribute("aria-label")!.Should().Be("Price Range");
    }

    [Fact]
    public void Slider_matches_shadcn_range_geometry_and_root_attributes()
    {
        var slider = Render<Slider>(parameters => parameters
            .Add(p => p.Values, new[] { 200d, 800d })
            .Add(p => p.Min, 0d)
            .Add(p => p.Max, 1000d)
            .Add(p => p.AriaLabel, "Price Range"));

        var root = slider.Find("[data-slot='slider']");
        var range = slider.Find("[data-slot='slider-range']");

        root.GetAttribute("dir")!.Should().Be("ltr");
        root.GetAttribute("aria-disabled")!.Should().Be("false");
        root.GetAttribute("style")!.Should().Contain("--radix-slider-thumb-transform: translateX(-50%)");
        root.HasAttribute("data-js-ready").Should().BeFalse();

        range.GetAttribute("style")!.Should().Contain("left: 20.00%");
        range.GetAttribute("style")!.Should().Contain("right: 20.00%");
        range.GetAttribute("style")!.Should().NotContain("width:");
    }
}
