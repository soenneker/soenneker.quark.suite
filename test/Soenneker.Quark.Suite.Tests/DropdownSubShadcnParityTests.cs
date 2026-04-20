using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DropdownSub_matches_shadcn_base_classes()
    {
        var sub = Render<DropdownSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        var subClasses = sub.Find("[data-slot='dropdown-menu-sub']").GetAttribute("class")!;

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-dropdown-sub");
    }
}
