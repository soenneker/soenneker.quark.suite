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
        var slider = Render<Slider>(parameters => parameters.Add(p => p.SliderValue, 75d));

        string sliderClasses = slider.Find("[data-slot='slider']").GetAttribute("class")!;
        string sliderTrackClasses = slider.Find("[data-slot='slider-track']").GetAttribute("class")!;
        string sliderRangeClasses = slider.Find("[data-slot='slider-range']").GetAttribute("class")!;
        string sliderThumbClasses = slider.Find("[data-slot='slider-thumb']").GetAttribute("class")!;

        sliderClasses.Should().Contain("relative");
        sliderClasses.Should().Contain("flex");
        sliderClasses.Should().Contain("touch-none");
        sliderClasses.Should().Contain("data-disabled:opacity-50");
        sliderClasses.Should().NotContain("q-slider");

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
    }
}
