using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void CardDescription_matches_shadcn_base_classes()
    {
        var description = Render<CardDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        string descriptionClasses = description.Find("[data-slot='card-description']").GetAttribute("class")!;

        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().NotContain("q-card-description");
    }
}
