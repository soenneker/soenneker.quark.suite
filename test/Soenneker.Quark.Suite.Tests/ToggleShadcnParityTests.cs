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

        toggleClasses.Should().NotContain("group/toggle");
        toggleClasses.Should().Contain("inline-flex");
        toggleClasses.Should().Contain("items-center");
        toggleClasses.Should().Contain("justify-center");
        toggleClasses.Should().Contain("gap-2");
        toggleClasses.Should().Contain("rounded-md");
        toggleClasses.Should().Contain("text-sm");
        toggleClasses.Should().Contain("font-medium");
        toggleClasses.Should().Contain("whitespace-nowrap");
        toggleClasses.Should().Contain("transition-[color,box-shadow]");
        toggleClasses.Should().Contain("outline-none");
        toggleClasses.Should().Contain("hover:bg-muted");
        toggleClasses.Should().Contain("hover:text-muted-foreground");
        toggleClasses.Should().Contain("focus-visible:ring-[3px]");
        toggleClasses.Should().NotContain("aria-pressed:bg-muted");
        toggleClasses.Should().Contain("data-[state=on]:bg-accent");
        toggleClasses.Should().Contain("data-[state=on]:text-accent-foreground");
        toggleClasses.Should().Contain("h-9");
        toggleClasses.Should().Contain("min-w-9");
        toggleClasses.Should().Contain("px-2");
        toggleClasses.Should().NotContain("gap-1");
        toggleClasses.Should().NotContain("rounded-lg");
        toggleClasses.Should().NotContain("hover:text-foreground");
        toggleClasses.Should().NotContain("transition-all");
        toggleClasses.Should().NotContain("data-[state=on]:bg-muted");
        toggleClasses.Should().NotContain("h-8");
        toggleClasses.Should().NotContain("min-w-8");
        toggleClasses.Should().NotContain("q-toggle");
    }
}
