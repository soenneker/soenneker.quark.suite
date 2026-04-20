using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SheetFooter_matches_shadcn_default_component_contract()
    {
        var footer = Render<SheetFooter>(parameters => parameters
            .Add(p => p.ChildContent, "Footer"));

        var footerClasses = footer.Find("[data-slot='sheet-footer']").GetAttribute("class")!;

        footerClasses.Should().Contain("mt-auto");
        footerClasses.Should().Contain("flex");
        footerClasses.Should().Contain("flex-col");
        footerClasses.Should().Contain("gap-2");
        footerClasses.Should().Contain("p-4");
    }
}
