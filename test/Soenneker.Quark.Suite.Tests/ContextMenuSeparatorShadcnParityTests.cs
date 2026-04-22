using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ContextMenuSeparator_matches_shadcn_base_classes()
    {
        var separator = Render<ContextMenuSeparator>();

        var separatorClasses = separator.Find("[data-slot='context-menu-separator']").GetAttribute("class")!;

        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("my-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-context-menu-separator");
    }
}
