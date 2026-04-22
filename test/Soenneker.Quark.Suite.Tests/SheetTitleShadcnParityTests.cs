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

        titleClasses.Should().Contain("cn-font-heading");
        titleClasses.Should().Contain("text-base");
        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("text-foreground");
    }
}
