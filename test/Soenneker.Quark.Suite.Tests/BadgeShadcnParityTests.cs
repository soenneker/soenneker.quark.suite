using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Badge_matches_shadcn_base_classes()
    {
        var cut = Render<Badge>(parameters => parameters
            .Add(p => p.Variant, BadgeVariant.Secondary)
            .Add(p => p.ChildContent, "Featured"));

        var classes = cut.Find("[data-slot='badge']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("rounded-full");
        classes.Should().Contain("border");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-0.5");
        classes.Should().Contain("text-xs");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("transition-[color,box-shadow]");
        classes.Should().Contain("[&>svg]:size-3");
        classes.Should().Contain("bg-secondary");
        classes.Should().Contain("text-secondary-foreground");
        classes.Should().Contain("[a&]:hover:bg-secondary/90");
        classes.Should().NotContain("group/badge");
        classes.Should().NotContain("h-5");
        classes.Should().NotContain("rounded-4xl");
        classes.Should().NotContain("transition-all");
        classes.Should().NotContain("has-data-[icon=inline-end]:pr-1.5");
        classes.Should().NotContain("has-data-[icon=inline-start]:pl-1.5");
        classes.Should().NotContain("q-badge");
    }
}
