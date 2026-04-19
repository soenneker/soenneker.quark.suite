using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Card_slots_match_shadcn_base_classes()
    {
        var header = Render<CardHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        string headerClasses = header.Find("[data-slot='card-header']").GetAttribute("class")!;

        headerClasses.Should().Contain("group/card-header");
        headerClasses.Should().Contain("@container/card-header");
        headerClasses.Should().Contain("gap-1");
        headerClasses.Should().Contain("px-4");
        headerClasses.Should().Contain("rounded-t-xl");
        headerClasses.Should().Contain("has-data-[slot=card-description]:grid-rows-[auto_auto]");
        headerClasses.Should().NotContain("q-card-header");
    }
}
