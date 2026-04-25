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
        classes.Should().Contain("group/button");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("border-transparent");
        classes.Should().Contain("bg-clip-padding");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("whitespace-nowrap");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("outline-none");
        classes.Should().Contain("select-none");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-3");
        classes.Should().Contain("active:not-aria-[haspopup]:translate-y-px");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
        classes.Should().Contain("has-data-[icon=inline-end]:pr-2");
        classes.Should().Contain("has-data-[icon=inline-start]:pl-2");
        classes.Should().Contain("bg-primary");
        classes.Should().Contain("text-primary-foreground");
        classes.Should().Contain("[a]:hover:bg-primary/80");
        classes.Should().NotContain("hover:bg-primary/90");
        classes.Should().NotContain("focus-visible:ring-[3px]");
        classes.Should().NotContain("gap-2");
        classes.Should().NotContain("rounded-md");
        classes.Should().NotContain("h-9");
        classes.Should().NotContain("px-4");
        classes.Should().NotContain("py-2");
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
        classes.Should().Contain("group/button");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("bg-clip-padding");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("select-none");
        classes.Should().Contain("focus-visible:ring-3");
        classes.Should().Contain("border");
        classes.Should().Contain("border-border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("hover:text-foreground");
        classes.Should().Contain("aria-expanded:bg-muted");
        classes.Should().Contain("aria-expanded:text-foreground");
        classes.Should().Contain("dark:border-input");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().Contain("dark:hover:bg-input/50");
        classes.Should().Contain("size-8");
        classes.Should().NotContain("border-1");
        classes.Should().NotContain("rounded-md");
        classes.Should().NotContain("focus-visible:ring-[3px]");
        classes.Should().NotContain("shadow-xs");
        classes.Should().NotContain("hover:bg-accent");
        classes.Should().NotContain("hover:text-accent-foreground");
        classes.Should().NotContain("size-9");
    }

}

