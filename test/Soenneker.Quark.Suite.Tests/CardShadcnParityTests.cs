using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Card_matches_shadcn_base_classes()
    {
        var cut = Render<Card>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        var card = cut.Find("[data-slot='card']");
        var classes = card.GetAttribute("class")!;

        card.GetAttribute("data-size").Should().BeNull();
        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("gap-6");
        classes.Should().Contain("rounded-xl");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-card");
        classes.Should().Contain("py-6");
        classes.Should().Contain("text-card-foreground");
        classes.Should().Contain("shadow-sm");
        classes.Should().NotContain("ring-1");
        classes.Should().NotContain("group/card");
    }

    [Test]
    public void Card_slots_match_shadcn_base_classes()
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
        headerClasses.Should().NotContain("q-card-header");
    }

    [Test]
    public void CardContent_matches_shadcn_base_classes()
    {
        var cut = Render<CardContent>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        var classes = cut.Find("[data-slot='card-content']").GetAttribute("class")!;

        classes.Should().Contain("px-6");
        classes.Should().NotContain("group-data-[size=sm]/card:px-3");
    }
}
