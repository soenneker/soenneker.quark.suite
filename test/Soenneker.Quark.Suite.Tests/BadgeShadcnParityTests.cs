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
        classes.Should().Contain("h-5");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("gap-1");
        classes.Should().Contain("overflow-hidden");
        classes.Should().Contain("rounded-4xl");
        classes.Should().Contain("border");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-0.5");
        classes.Should().Contain("text-xs");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("whitespace-nowrap");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().Contain("focus-visible:ring-ring/50");
        classes.Should().Contain("[&>svg]:pointer-events-none");
        classes.Should().Contain("[&>svg]:size-3!");
        classes.Should().Contain("bg-secondary");
        classes.Should().Contain("text-secondary-foreground");
        classes.Should().Contain("[a]:hover:bg-secondary/80");
        classes.Should().Contain("group/badge");
        classes.Should().Contain("has-data-[icon=inline-end]:pr-1.5");
        classes.Should().Contain("has-data-[icon=inline-start]:pl-1.5");
        classes.Should().NotContain("q-badge");
    }

    [Test]
    public void Badge_link_variant_does_not_apply_default_background()
    {
        var cut = Render<Badge>(parameters => parameters
            .Add(p => p.Variant, BadgeVariant.Link)
            .Add(p => p.ChildContent, "Link"));

        var classes = cut.Find("[data-slot='badge']").GetAttribute("class")!;

        classes.Should().Contain("text-primary");
        classes.Should().Contain("underline-offset-4");
        classes.Should().Contain("[a]:hover:underline");
        classes.Should().NotContain("bg-primary");
        classes.Should().NotContain("text-primary-foreground");
    }
}
