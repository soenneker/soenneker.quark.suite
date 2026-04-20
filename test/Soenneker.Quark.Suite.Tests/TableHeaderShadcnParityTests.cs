using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Thead_matches_shadcn_base_classes()
    {
        var thead = Render<Thead>(parameters => parameters.Add(p => p.ChildContent, "Head"));

        var theadClasses = thead.Find("[data-slot='table-header']").GetAttribute("class")!;

        theadClasses.Should().Contain("[&_tr]:border-b");
        theadClasses.Should().NotContain("q-table-thead");
    }
}
