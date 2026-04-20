using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ContextMenuLabel_matches_shadcn_base_classes()
    {
        var label = Render<ContextMenuLabel>(parameters => parameters.Add(p => p.ChildContent, "My Account"));

        var labelClasses = label.Find("[data-slot='context-menu-label']").GetAttribute("class")!;

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-sm");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().NotContain("q-context-menu-label");
    }
}
