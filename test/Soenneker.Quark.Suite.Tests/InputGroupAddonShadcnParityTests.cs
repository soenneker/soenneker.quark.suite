using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void InputGroupAddon_matches_shadcn_base_classes()
    {
        var cut = Render<InputGroupAddon>(parameters => parameters
            .Add(p => p.ChildContent, "https://"));

        var classes = cut.Find("[data-slot='input-group-addon']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("h-auto");
        classes.Should().Contain("cursor-text");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("pl-2");
        classes.Should().Contain("has-[>button]:ml-[-0.3rem]");
        classes.Should().Contain("has-[>kbd]:ml-[-0.15rem]");
        classes.Should().NotContain("pl-3");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("select-none");
        classes.Should().Contain("[&>kbd]:rounded-[calc(var(--radius)-5px)]");
        classes.Should().NotContain("q-input-group-addon");
    }
}
