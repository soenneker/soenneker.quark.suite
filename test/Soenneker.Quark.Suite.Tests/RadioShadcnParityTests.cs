using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Radio_matches_shadcn_item_contract()
    {
        var cut = Render<Radio>(parameters => parameters
            .Add(p => p.Checked, true)
            .Add(p => p.Value, "kubernetes"));

        var classes = cut.Find("[data-slot='radio-group-item']").GetAttribute("class")!;
        var indicatorClasses = cut.Find("[data-slot='radio-group-indicator']").GetAttribute("class")!;
        var iconClasses = cut.Find("[data-slot='icon']").GetAttribute("class")!;

        classes.Should().NotContain("group/radio-group-item");
        classes.Should().NotContain("peer");
        classes.Should().NotContain("relative");
        classes.Should().NotContain("flex");
        classes.Should().Contain("aspect-square");
        classes.Should().Contain("size-4");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("rounded-full");
        classes.Should().Contain("border");
        classes.Should().Contain("border-input");
        classes.Should().Contain("outline-none");
        classes.Should().NotContain("after:absolute");
        classes.Should().NotContain("after:-inset-x-3");
        classes.Should().NotContain("after:-inset-y-2");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().Contain("aria-invalid:ring-destructive/20");
        classes.Should().NotContain("aria-invalid:aria-checked:border-primary");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().NotContain("data-checked:border-primary");
        classes.Should().NotContain("data-checked:bg-primary");
        classes.Should().NotContain("data-checked:text-primary-foreground");
        classes.Should().Contain("shadow-xs");
        classes.Should().Contain("transition-[color,box-shadow]");

        indicatorClasses.Should().Contain("relative");
        indicatorClasses.Should().Contain("flex");
        indicatorClasses.Should().Contain("items-center");
        indicatorClasses.Should().Contain("justify-center");
        iconClasses.Should().Contain("absolute");
        iconClasses.Should().Contain("h-2");
        iconClasses.Should().Contain("w-2");
        iconClasses.Should().Contain("fill-primary");
    }
}
