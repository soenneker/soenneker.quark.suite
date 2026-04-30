using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void CardTitle_matches_shadcn_base_classes()
    {
        var title = Render<CardTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));

        var titleClasses = title.Find("[data-slot='card-title']").GetAttribute("class")!;

        titleClasses.Should().Contain("leading-none");
        titleClasses.Should().Contain("font-semibold");
        titleClasses.Should().NotContain("cn-font-heading");
        titleClasses.Should().NotContain("text-base");
        titleClasses.Should().NotContain("leading-snug");
        titleClasses.Should().NotContain("font-medium");
        titleClasses.Should().NotContain("group-data-[size=sm]/card:text-sm");
        titleClasses.Should().NotContain("q-card-title");
    }
}
