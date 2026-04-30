using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void CardHeader_matches_shadcn_base_classes()
    {
        var header = Render<CardHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        var headerClasses = header.Find("[data-slot='card-header']").GetAttribute("class")!;

        headerClasses.Should().Contain("@container/card-header");
        headerClasses.Should().Contain("grid");
        headerClasses.Should().Contain("auto-rows-min");
        headerClasses.Should().Contain("items-start");
        headerClasses.Should().Contain("gap-1");
        headerClasses.Should().Contain("rounded-t-xl");
        headerClasses.Should().Contain("px-4");
        headerClasses.Should().Contain("group-data-[size=sm]/card:px-3");
        headerClasses.Should().Contain("has-data-[slot=card-action]:grid-cols-[1fr_auto]");
        headerClasses.Should().Contain("has-data-[slot=card-description]:grid-rows-[auto_auto]");
        headerClasses.Should().Contain("[.border-b]:pb-4");
        headerClasses.Should().NotContain("[.border-b]:pb-6");
        headerClasses.Should().NotContain("q-card-header");
    }
}
