using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Separator_matches_shadcn_base_classes()
    {
        var cut = Render<Hr>(parameters => parameters
            .Add(p => p.Orientation, SeparatorOrientation.Horizontal));

        var classes = cut.Find("[data-slot='separator']").GetAttribute("class")!;

        classes.Should().Contain("bg-border");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("data-[orientation=horizontal]:h-px");
        classes.Should().Contain("data-[orientation=horizontal]:w-full");
        classes.Should().Contain("data-[orientation=vertical]:h-full");
        classes.Should().Contain("data-[orientation=vertical]:w-px");
        classes.Should().NotContain("data-horizontal:");
        classes.Should().NotContain("data-vertical:");
        classes.Should().NotContain("q-hr");
    }

    [Test]
    public void Separator_composes_bradix_semantics_with_shadcn_decorative_default()
    {
        var cut = Render<Separator>();

        var separator = cut.Find("[data-slot='separator']");

        separator.GetAttribute("role").Should().Be("none");
        separator.GetAttribute("data-orientation").Should().Be("horizontal");
        separator.GetAttribute("aria-orientation").Should().BeNull();
    }

    [Test]
    public void Separator_semantic_vertical_uses_radix_aria_orientation()
    {
        var cut = Render<Separator>(parameters => parameters
            .Add(p => p.Decorative, false)
            .Add(p => p.Orientation, SeparatorOrientation.Vertical));

        var separator = cut.Find("[data-slot='separator']");

        separator.GetAttribute("role").Should().Be("separator");
        separator.GetAttribute("data-orientation").Should().Be("vertical");
        separator.GetAttribute("aria-orientation").Should().Be("vertical");
    }
}
