using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SheetDescription_matches_shadcn_default_component_contract()
    {
        var description = Render<SheetDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        string descriptionClasses = description.Find("[data-slot='sheet-description']").GetAttribute("class")!;

        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("text-muted-foreground");
    }
}
