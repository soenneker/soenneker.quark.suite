using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests{
    [Fact]
    public void Button_default_matches_shadcn_base_classes_exactly()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.ChildContent, "Button"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Contain("group/button");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("rounded-[min(var(--radius-md),12px)]");
        classes.Should().Contain("bg-clip-padding");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("whitespace-nowrap");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("select-none");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-3");
        classes.Should().Contain("[a]:hover:bg-primary/80");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
        classes.Should().Contain("has-data-[icon=inline-end]:pr-2");
        classes.Should().Contain("has-data-[icon=inline-start]:pl-2");
        classes.Should().Contain("bg-primary");
        classes.Should().Contain("text-primary-foreground");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("border");
        classes.Should().Contain("border-transparent");
        classes.Should().NotContain("rounded-lg!");
        classes.Should().NotContain("border-1");
    }


    [Fact]
    public void Button_outline_icon_matches_shadcn_base_classes_exactly()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Outline)
            .Add(p => p.Size, ButtonSize.Icon)
            .AddUnmatched("aria-label", "Submit")
            .Add(p => p.ChildContent, "Icon"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Be("group/button inline-flex shrink-0 items-center justify-center border bg-clip-padding font-medium whitespace-nowrap transition-all outline-none select-none focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50 active:not-aria-[haspopup]:translate-y-px disabled:pointer-events-none disabled:opacity-50 aria-invalid:border-destructive aria-invalid:ring-3 aria-invalid:ring-destructive/20 dark:aria-invalid:border-destructive/50 dark:aria-invalid:ring-destructive/40 [&_svg]:pointer-events-none [&_svg]:shrink-0 [&_svg:not([class*='size-'])]:size-4 hover:bg-muted hover:text-foreground aria-expanded:bg-muted aria-expanded:text-foreground dark:border-input dark:bg-input/30 dark:hover:bg-input/50 size-8 text-sm rounded-[min(var(--radius-md),12px)] in-data-[slot=button-group]:rounded-lg border-border bg-background");
        classes.Should().NotContain("border-1");
        classes.Should().NotContain("inline-flex text-sm font-medium");
    }

}

