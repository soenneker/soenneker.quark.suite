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

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-sm");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().Contain("data-[inset]:pl-8");
        labelClasses.Should().NotContain("text-muted-foreground");
        labelClasses.Should().NotContain("q-dropdown-label");
    }
}
