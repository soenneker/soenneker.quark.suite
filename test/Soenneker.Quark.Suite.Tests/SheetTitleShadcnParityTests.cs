using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SheetTitle_matches_shadcn_default_component_contract()
    {
        var title = Render<SheetTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));

        var titleClasses = title.Find("[data-slot='sheet-title']").GetAttribute("class")!;

        titleClasses.Should().Contain("font-semibold");
        titleClasses.Should().Contain("text-foreground");
        titleClasses.Should().NotContain("cn-font-heading");
        titleClasses.Should().NotContain("text-base");
        titleClasses.Should().NotContain("font-medium");
    }
}
