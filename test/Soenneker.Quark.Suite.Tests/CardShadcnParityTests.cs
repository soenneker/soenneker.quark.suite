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

        card.GetAttribute("data-size").Should().Be("default");
        classes.Should().Contain("group/card");
        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("gap-4");
        classes.Should().Contain("overflow-hidden");
        classes.Should().Contain("rounded-xl");
        classes.Should().Contain("bg-card");
        classes.Should().Contain("py-4");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-card-foreground");
        classes.Should().Contain("ring-1");
        classes.Should().Contain("ring-foreground/10");
        classes.Should().Contain("has-data-[slot=card-footer]:pb-0");
        classes.Should().Contain("data-[size=sm]:gap-3");
        classes.Should().NotContain("border");
        classes.Should().NotContain("gap-6");
        classes.Should().NotContain("py-6");
        classes.Should().NotContain("shadow-sm");
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
        headerClasses.Should().Contain("items-start");
        headerClasses.Should().Contain("gap-1");
        headerClasses.Should().Contain("rounded-t-xl");
        headerClasses.Should().Contain("px-4");
        headerClasses.Should().Contain("group-data-[size=sm]/card:px-3");
        headerClasses.Should().Contain("has-data-[slot=card-description]:grid-rows-[auto_auto]");
        headerClasses.Should().Contain("[.border-b]:pb-4");
        headerClasses.Should().NotContain("gap-2");
        headerClasses.Should().NotContain("px-6");
        headerClasses.Should().NotContain("q-card-header");
    }

    [Test]
    public void CardContent_matches_shadcn_base_classes()
    {
        var cut = Render<CardContent>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        var classes = cut.Find("[data-slot='card-content']").GetAttribute("class")!;

        classes.Should().Contain("px-4");
        classes.Should().Contain("group-data-[size=sm]/card:px-3");
        classes.Should().NotContain("px-6");
    }
}
