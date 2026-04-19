using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Tbody_matches_shadcn_base_classes()
    {
        var tbody = Render<Tbody>(parameters => parameters.Add(p => p.ChildContent, "Body"));

        string tbodyClasses = tbody.Find("[data-slot='table-body']").GetAttribute("class")!;

        tbodyClasses.Should().Contain("[&_tr:last-child]:border-0");
        tbodyClasses.Should().NotContain("q-table-tbody");
    }
}
