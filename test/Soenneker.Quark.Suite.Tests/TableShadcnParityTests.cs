using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Table_slots_match_shadcn_base_classes()
    {
        var table = Render<TableElement>(parameters => parameters.Add(p => p.ChildContent, "Rows"));

        string tableContainerClasses = table.Find("[data-slot='table-container']").GetAttribute("class")!;
        string tableClasses = table.Find("[data-slot='table']").GetAttribute("class")!;

        tableContainerClasses.Should().Contain("relative");
        tableContainerClasses.Should().Contain("w-full");
        tableContainerClasses.Should().Contain("overflow-x-auto");
        tableContainerClasses.Should().NotContain("q-table-container");

        tableClasses.Should().Contain("caption-bottom");
        tableClasses.Should().NotContain("q-table");
    }
}
