using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void MenubarLabel_matches_shadcn_base_classes()
    {
        var label = Render<MenubarLabel>(parameters => parameters.Add(p => p.ChildContent, "My Account"));

        var labelClasses = label.Find("[data-slot='menubar-label']").GetAttribute("class")!;

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-sm");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().NotContain("q-menubar-label");
    }
}
