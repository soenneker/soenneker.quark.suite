using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ContextMenuTrigger_matches_shadcn_base_classes()
    {
        var cut = Render<ContextMenuTrigger>(parameters => parameters.Add(p => p.ChildContent, "Open"));

        var classes = cut.Find("[data-slot='context-menu-trigger']").GetAttribute("class");

        (classes ?? string.Empty).Should().NotContain("q-context-menu-trigger");
        (classes ?? string.Empty).Should().NotContain("cursor-context-menu");
    }
}
