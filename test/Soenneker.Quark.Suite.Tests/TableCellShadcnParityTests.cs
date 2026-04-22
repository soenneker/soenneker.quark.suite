using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Td_matches_shadcn_base_classes()
    {
        var td = Render<Td>(parameters => parameters.Add(p => p.ChildContent, "Cell"));

        var tdClasses = td.Find("[data-slot='table-cell']").GetAttribute("class")!;

        tdClasses.Should().Contain("p-2");
        tdClasses.Should().Contain("align-middle");
        tdClasses.Should().Contain("whitespace-nowrap");
        tdClasses.Should().NotContain("q-table-td");
    }
}
