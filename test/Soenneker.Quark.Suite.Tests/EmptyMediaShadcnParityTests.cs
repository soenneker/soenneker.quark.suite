using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void EmptyMedia_icon_variant_matches_shadcn_base_classes()
    {
        var cut = Render<EmptyMedia>(parameters => parameters
            .Add(p => p.Variant, EmptyMediaVariant.Icon)
            .Add(p => p.ChildContent, "Icon"));

        var media = cut.Find("[data-slot='empty-icon']");
        var classes = media.GetAttribute("class")!;

        media.GetAttribute("data-variant").Should().Be("icon");
        classes.Should().Contain("mb-2");
        classes.Should().Contain("flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("size-10");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("bg-muted");
        classes.Should().Contain("text-foreground");
        classes.Should().Contain("[&_svg:not([class*='size-'])]:size-6");
        classes.Should().NotContain("size-8");
        classes.Should().NotContain("[&_svg:not([class*='size-'])]:size-4");
    }
}
