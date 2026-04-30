using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Button_default_matches_shadcn_base_classes_exactly()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.ChildContent, "Button"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("whitespace-nowrap");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("outline-none");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().Contain("h-9");
        classes.Should().Contain("px-4");
        classes.Should().Contain("py-2");
        classes.Should().Contain("has-[>svg]:px-3");
        classes.Should().Contain("bg-primary");
        classes.Should().Contain("text-primary-foreground");
        classes.Should().Contain("hover:bg-primary/90");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("rounded-lg");
        classes.Should().NotContain("border-transparent");
        classes.Should().NotContain("bg-clip-padding");
        classes.Should().NotContain("select-none");
        classes.Should().NotContain("active:not-aria-[haspopup]:translate-y-px");
        classes.Should().NotContain("h-8");
        classes.Should().NotContain("px-2.5");
        classes.Should().NotContain("[a]:hover:bg-primary/80");
        classes.Should().NotContain("rounded-lg!");
        classes.Should().NotContain("border-1");
    }


    [Test]
    public void Button_outline_icon_matches_shadcn_base_classes_exactly()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Outline)
            .Add(p => p.Size, ButtonSize.Icon)
            .AddUnmatched("aria-label", "Submit")
            .Add(p => p.ChildContent, "Icon"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("shadow-xs");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("dark:border-input");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().Contain("dark:hover:bg-input/50");
        classes.Should().Contain("size-9");
        classes.Should().NotContain("border-1");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("rounded-lg");
        classes.Should().NotContain("select-none");
        classes.Should().NotContain("focus-visible:ring-3");
        classes.Should().NotContain("border-border");
        classes.Should().NotContain("hover:bg-muted");
        classes.Should().NotContain("hover:text-foreground");
        classes.Should().NotContain("aria-expanded:bg-muted");
        classes.Should().NotContain("aria-expanded:text-foreground");
        classes.Should().NotContain("size-8");
    }

}

