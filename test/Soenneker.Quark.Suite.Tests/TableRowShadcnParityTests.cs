using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Tr_matches_shadcn_base_classes()
    {
        var tr = Render<Tr>(parameters => parameters.Add(p => p.ChildContent, "Row"));

        var trClasses = tr.Find("[data-slot='table-row']").GetAttribute("class")!;

        trClasses.Should().Contain("hover:bg-muted/50");
        trClasses.Should().Contain("data-[state=selected]:bg-muted");
        trClasses.Should().Contain("border-b");
        trClasses.Should().NotContain("q-table-tr");
    }
}
