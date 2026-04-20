using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Badge_matches_shadcn_base_classes()
    {
        var cut = Render<Badge>(parameters => parameters
            .Add(p => p.Variant, BadgeVariant.Secondary)
            .Add(p => p.ChildContent, "Featured"));

        var classes = cut.Find("[data-slot='badge']").GetAttribute("class")!;

        classes.Should().Contain("group/badge");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("h-5");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("rounded-4xl");
        classes.Should().Contain("border");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-0.5");
        classes.Should().Contain("text-xs");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("has-data-[icon=inline-end]:pr-1.5");
        classes.Should().Contain("has-data-[icon=inline-start]:pl-1.5");
        classes.Should().Contain("[&>svg]:size-3!");
        classes.Should().Contain("bg-secondary");
        classes.Should().Contain("text-secondary-foreground");
        classes.Should().Contain("[a]:hover:bg-secondary/80");
        classes.Should().NotContain("q-badge");
    }
}
