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

        classes.Should().Contain("group/radio-group-item");
        classes.Should().Contain("peer");
        classes.Should().Contain("relative");
        classes.Should().Contain("flex");
        classes.Should().Contain("aspect-square");
        classes.Should().Contain("size-4");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("rounded-full");
        classes.Should().Contain("border");
        classes.Should().Contain("border-input");
        classes.Should().Contain("outline-none");
        classes.Should().Contain("after:absolute");
        classes.Should().Contain("after:-inset-x-3");
        classes.Should().Contain("after:-inset-y-2");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-3");
        classes.Should().Contain("aria-invalid:ring-destructive/20");
        classes.Should().Contain("aria-invalid:aria-checked:border-primary");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().Contain("data-checked:border-primary");
        classes.Should().Contain("data-checked:bg-primary");
        classes.Should().Contain("data-checked:text-primary-foreground");
        classes.Should().NotContain("shadow-xs");
        classes.Should().NotContain("transition-[color,box-shadow]");
        classes.Should().NotContain("focus-visible:ring-[3px]");

        indicatorClasses.Should().Contain("relative");
        indicatorClasses.Should().Contain("flex");
        indicatorClasses.Should().Contain("items-center");
        indicatorClasses.Should().Contain("justify-center");
        iconClasses.Should().Contain("absolute");
        iconClasses.Should().Contain("size-2");
        iconClasses.Should().Contain("fill-primary");
    }
}
