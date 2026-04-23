using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Slider_matches_shadcn_base_classes()
    {
        var slider = Render<Slider>(parameters => parameters
            .Add(p => p.SliderValue, 75d)
            .Add(p => p.AriaLabel, "Volume"));

        var sliderClasses = slider.Find("[data-slot='slider']").GetAttribute("class")!;
        var sliderTrackClasses = slider.Find("[data-slot='slider-track']").GetAttribute("class")!;
        var sliderRangeClasses = slider.Find("[data-slot='slider-range']").GetAttribute("class")!;
        var sliderThumbClasses = slider.Find("[data-slot='slider-thumb']").GetAttribute("class")!;

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
        sliderRangeClasses.Should().Contain("data-horizontal:h-full");

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

    [Test]
    public void Slider_thumbs_use_in_bounds_offsets_for_absolute_wrappers()
    {
        var slider = Render<Slider>(parameters => parameters
            .Add(p => p.Values, new[] { 200d, 800d })
            .Add(p => p.Min, 0d)
            .Add(p => p.Max, 1000d)
            .Add(p => p.AriaLabel, "Price Range"));

        var thumbs = slider.FindAll("[data-slot='slider-thumb']");
        var minimumWrapper = thumbs[0].ParentElement;
        var maximumWrapper = thumbs[1].ParentElement;

        minimumWrapper.Should().NotBeNull();
        minimumWrapper!.TagName.Should().Be("SPAN");
        minimumWrapper.GetAttribute("style").Should().Contain("position: absolute");
        minimumWrapper.GetAttribute("style").Should().Contain("left: calc(20.00% + 3.6px)");
        minimumWrapper.GetAttribute("style").Should().Contain("transform: var(--radix-slider-thumb-transform)");

        maximumWrapper.Should().NotBeNull();
        maximumWrapper!.GetAttribute("style").Should().Contain("left: calc(80.00% - 3.6px)");
        maximumWrapper.GetAttribute("style").Should().Contain("transform: var(--radix-slider-thumb-transform)");

        thumbs[0].GetAttribute("style").Should().BeNull();
        thumbs[1].GetAttribute("style").Should().BeNull();
    }

    [Test]
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

    [Test]
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
        root.HasAttribute("data-horizontal").Should().BeTrue();
        range.HasAttribute("data-horizontal").Should().BeTrue();
        root.GetAttribute("style")!.Should().Contain("--radix-slider-thumb-transform: translateX(-50%)");
        root.GetAttribute("data-js-ready")!.Should().Be("true");

        range.GetAttribute("style")!.Should().Contain("left: 20.00%");
        range.GetAttribute("style")!.Should().Contain("right: 20.00%");
        range.GetAttribute("style")!.Should().NotContain("width:");
    }
}
