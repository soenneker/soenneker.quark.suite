using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
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
        sliderThumbClasses.Should().NotContain("q-slider");
        slider.Find("[data-slot='slider-thumb']").HasAttribute("aria-label").Should().BeFalse();
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
}
