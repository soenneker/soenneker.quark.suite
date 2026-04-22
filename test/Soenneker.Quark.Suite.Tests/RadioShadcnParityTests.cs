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

        classes.Should().Contain("group/radio-group-item");
        classes.Should().Contain("rounded-full");
        classes.Should().Contain("border");
        classes.Should().Contain("border-input");
        classes.Should().Contain("aria-invalid:ring-3");
        classes.Should().Contain("data-[state=checked]:border-primary");
        classes.Should().Contain("data-[state=checked]:bg-primary");
        classes.Should().Contain("data-[state=checked]:text-primary-foreground");
        classes.Should().Contain("dark:data-[state=checked]:bg-primary");
        classes.Should().NotContain("shadow-xs");
        classes.Should().NotContain(" transition-[color,box-shadow] ");
    }
}
