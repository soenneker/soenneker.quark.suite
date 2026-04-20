using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SelectGroup_matches_shadcn_base_classes()
    {
        var group = Render<SelectGroup>(parameters => parameters.Add(p => p.ChildContent, "Items"));

        string groupClasses = group.Find("[data-slot='select-group']").GetAttribute("class")!;

        groupClasses.Should().Contain("p-1");
        groupClasses.Should().NotContain("q-select-group");
    }
}
