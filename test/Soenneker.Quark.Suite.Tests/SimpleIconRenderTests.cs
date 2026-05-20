using AwesomeAssertions;
using Bunit;
using Soenneker.SimpleIcons.Enums.Icons;
using QuarkSimpleIcon = Soenneker.Quark.SimpleIcon;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SimpleIcon_renders_provider_svg_with_quark_attributes()
    {
        var cut = Render<QuarkSimpleIcon>(parameters => parameters
            .Add(p => p.Name, SimpleIconEnum.Github)
            .Add(p => p.Class, "text-primary")
            .AddUnmatched("aria-label", "GitHub"));

        var svg = cut.Find("svg");

        svg.GetAttribute("role").Should().Be("img");
        svg.GetAttribute("viewBox").Should().Be("0 0 24 24");
        svg.GetAttribute("class").Should().Contain("text-primary");
        svg.GetAttribute("class").Should().Contain("size-4");
        svg.GetAttribute("aria-label").Should().Be("GitHub");
    }
}
