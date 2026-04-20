using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ItemDescription_matches_shadcn_base_classes()
    {
        var description = Render<ItemDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        string descriptionClasses = description.Find("[data-slot='item-description']").GetAttribute("class")!;

        descriptionClasses.Should().Contain("line-clamp-2");
        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("leading-normal");
        descriptionClasses.Should().Contain("font-normal");
        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().Contain("group-data-[size=xs]/item:text-xs");
        descriptionClasses.Should().Contain("[&>a]:underline-offset-4");
        descriptionClasses.Should().NotContain("q-item-description");
    }
}
