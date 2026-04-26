using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DropdownLabel_matches_shadcn_base_classes()
    {
        var label = Render<DropdownLabel>(parameters => parameters.Add(p => p.ChildContent, "My Account"));

        var labelClasses = label.Find("[data-slot='dropdown-menu-label']").GetAttribute("class")!;

        labelClasses.Should().Contain("px-1.5");
        labelClasses.Should().Contain("py-1");
        labelClasses.Should().Contain("text-xs");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().NotContain("q-dropdown-label");
    }
}
