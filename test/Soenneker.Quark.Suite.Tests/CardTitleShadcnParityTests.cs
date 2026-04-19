using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void CardTitle_matches_shadcn_base_classes()
    {
        var title = Render<CardTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));

        string titleClasses = title.Find("[data-slot='card-title']").GetAttribute("class")!;

        titleClasses.Should().Contain("cn-font-heading");
        titleClasses.Should().Contain("text-base");
        titleClasses.Should().Contain("leading-snug");
        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("group-data-[size=sm]/card:text-sm");
        titleClasses.Should().NotContain("q-card-title");
    }
}
