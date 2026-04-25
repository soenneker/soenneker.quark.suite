using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SheetHeader_matches_shadcn_default_component_contract()
    {
        var header = Render<SheetHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        var headerClasses = header.Find("[data-slot='sheet-header']").GetAttribute("class")!;

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("gap-1.5");
        headerClasses.Should().Contain("p-4");
    }
}
