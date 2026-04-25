using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Toggle_matches_shadcn_base_classes()
    {
        var toggle = Render<Toggle>(parameters => parameters.Add(p => p.ChildContent, "Italic"));

        var toggleClasses = toggle.Find("[data-slot='toggle']").GetAttribute("class")!;

        toggleClasses.Should().Contain("group/toggle");
        toggleClasses.Should().Contain("inline-flex");
        toggleClasses.Should().Contain("items-center");
        toggleClasses.Should().Contain("justify-center");
        toggleClasses.Should().Contain("gap-1");
        toggleClasses.Should().Contain("rounded-lg");
        toggleClasses.Should().Contain("text-sm");
        toggleClasses.Should().Contain("font-medium");
        toggleClasses.Should().Contain("whitespace-nowrap");
        toggleClasses.Should().Contain("transition-all");
        toggleClasses.Should().Contain("outline-none");
        toggleClasses.Should().Contain("hover:bg-muted");
        toggleClasses.Should().Contain("hover:text-foreground");
        toggleClasses.Should().Contain("focus-visible:ring-[3px]");
        toggleClasses.Should().Contain("aria-pressed:bg-muted");
        toggleClasses.Should().Contain("data-[state=on]:bg-muted");
        toggleClasses.Should().Contain("h-8");
        toggleClasses.Should().Contain("min-w-8");
        toggleClasses.Should().Contain("px-2.5");
        toggleClasses.Should().NotContain("gap-2");
        toggleClasses.Should().NotContain("rounded-md");
        toggleClasses.Should().NotContain("hover:text-muted-foreground");
        toggleClasses.Should().NotContain("transition-[color,box-shadow]");
        toggleClasses.Should().NotContain("data-[state=on]:bg-accent");
        toggleClasses.Should().NotContain("data-[state=on]:text-accent-foreground");
        toggleClasses.Should().NotContain("h-9");
        toggleClasses.Should().NotContain("min-w-9");
        toggleClasses.Should().NotContain("q-toggle");
    }
}
