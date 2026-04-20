using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Dropdown_leaf_slots_match_shadcn_base_classes()
    {
        var sub = Render<DropdownSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));
        var subClasses = sub.Find("[data-slot='dropdown-menu-sub']").GetAttribute("class")!;
        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-dropdown-sub");
    }

}
