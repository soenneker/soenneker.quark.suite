using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SheetDescription_matches_shadcn_default_component_contract()
    {
        var description = Render<SheetDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        var descriptionClasses = description.Find("[data-slot='sheet-description']").GetAttribute("class")!;

        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("text-muted-foreground");
    }
}
