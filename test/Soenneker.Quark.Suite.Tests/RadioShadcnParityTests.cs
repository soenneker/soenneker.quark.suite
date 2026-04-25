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

        classes.Should().Contain("aspect-square");
        classes.Should().Contain("size-4");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("rounded-full");
        classes.Should().Contain("border");
        classes.Should().Contain("border-input");
        classes.Should().Contain("text-primary");
        classes.Should().Contain("shadow-xs");
        classes.Should().Contain("transition-[color,box-shadow]");
        classes.Should().Contain("outline-none");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().Contain("aria-invalid:ring-destructive/20");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().NotContain("group/radio-group-item");
        classes.Should().NotContain("data-[state=checked]:bg-primary");

        indicatorClasses.Should().Contain("relative");
        indicatorClasses.Should().Contain("flex");
        indicatorClasses.Should().Contain("items-center");
        indicatorClasses.Should().Contain("justify-center");
        iconClasses.Should().Contain("absolute");
        iconClasses.Should().Contain("size-2");
        iconClasses.Should().Contain("fill-primary");
    }
}
