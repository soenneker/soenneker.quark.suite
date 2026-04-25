using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TableFooter_matches_shadcn_base_classes()
    {
        var footer = Render<TableFooter>(parameters => parameters.Add(p => p.ChildContent, "Footer"));

        var footerClasses = footer.Find("[data-slot='table-footer']").GetAttribute("class")!;

        footerClasses.Should().Contain("border-t");
        footerClasses.Should().Contain("bg-muted/50");
        footerClasses.Should().Contain("font-medium");
        footerClasses.Should().Contain("[&>tr]:last:border-b-0");
        footerClasses.Should().NotContain("q-table-footer");
    }
}
