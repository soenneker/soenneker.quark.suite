using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DropdownSeparator_matches_shadcn_base_classes()
    {
        var divider = Render<DropdownDivider>();

        string dividerClasses = divider.Find("[data-slot='dropdown-menu-separator']").GetAttribute("class")!;

        dividerClasses.Should().Contain("-mx-1");
        dividerClasses.Should().Contain("my-1");
        dividerClasses.Should().Contain("h-px");
        dividerClasses.Should().Contain("bg-border");
        dividerClasses.Should().NotContain("q-dropdown-divider");
    }
}
