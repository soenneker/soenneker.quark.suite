using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Kbd_matches_shadcn_base_classes()
    {
        var kbd = Render<Kbd>(parameters => parameters
            .Add(p => p.ChildContent, "Ctrl"));

        var kbdClasses = kbd.Find("[data-slot='kbd']").GetAttribute("class")!;

        kbdClasses.Should().Contain("pointer-events-none");
        kbdClasses.Should().Contain("inline-flex");
        kbdClasses.Should().Contain("h-5");
        kbdClasses.Should().Contain("w-fit");
        kbdClasses.Should().Contain("min-w-5");
        kbdClasses.Should().Contain("items-center");
        kbdClasses.Should().Contain("justify-center");
        kbdClasses.Should().Contain("gap-1");
        kbdClasses.Should().Contain("rounded-sm");
        kbdClasses.Should().Contain("bg-muted");
        kbdClasses.Should().Contain("px-1");
        kbdClasses.Should().Contain("font-sans");
        kbdClasses.Should().Contain("font-medium");
        kbdClasses.Should().Contain("text-xs");
        kbdClasses.Should().Contain("text-muted-foreground");
        kbdClasses.Should().Contain("select-none");
        kbdClasses.Should().Contain("in-data-[slot=tooltip-content]:bg-background/20");
        kbdClasses.Should().Contain("in-data-[slot=tooltip-content]:text-background");
        kbdClasses.Should().Contain("dark:in-data-[slot=tooltip-content]:bg-background/10");
        kbdClasses.Should().Contain("[&_svg:not([class*='size-'])]:size-3");
        kbdClasses.Should().NotContain("q-kbd");
    }
}
