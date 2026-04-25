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
        headerClasses.Should().Contain("grid-rows-[auto_auto]");
        headerClasses.Should().Contain("items-start");
        headerClasses.Should().Contain("gap-2");
        headerClasses.Should().Contain("px-6");
        headerClasses.Should().Contain("has-data-[slot=card-action]:grid-cols-[1fr_auto]");
        headerClasses.Should().Contain("[.border-b]:pb-6");
        headerClasses.Should().NotContain("q-card-header");
    }
}
