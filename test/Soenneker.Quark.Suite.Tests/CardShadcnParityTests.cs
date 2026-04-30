using AwesomeAssertions;
using Bunit;
using System;
using System.IO;


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
        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("group/card");
        classes.Should().Contain("gap-4");
        classes.Should().Contain("overflow-hidden");
        classes.Should().Contain("rounded-xl");
        classes.Should().Contain("bg-card");
        classes.Should().Contain("py-4");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-card-foreground");
        classes.Should().Contain("ring-1");
        classes.Should().Contain("ring-foreground/10");
        classes.Should().Contain("data-[size=sm]:gap-3");
        classes.Should().Contain("data-[size=sm]:py-3");
        classes.Should().NotContain("border");
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
        headerClasses.Should().Contain("has-data-[slot=card-action]:grid-cols-[1fr_auto]");
        headerClasses.Should().Contain("has-data-[slot=card-description]:grid-rows-[auto_auto]");
        headerClasses.Should().Contain("[.border-b]:pb-4");
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
    }

    [Test]
    public void Card_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForCard(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Cards.razor"));

        source.Should().Contain("Displays a card with header, content, and footer.");
        source.Should().Contain("Use the size=&quot;sm&quot; prop to set the size of the card to small.");
        source.Should().Contain("<CardTitle>Small Card</CardTitle>");
        source.Should().Contain("This card uses the small size variant.");
        source.Should().Contain("Add an image before the card header to create a card with an image.");
        source.Should().Contain("Alt=\"Event cover\"");
        source.Should().Contain("<Badge Variant=\"BadgeVariant.Secondary\">Featured</Badge>");
        source.Should().Contain("<CardTitle>Design systems meetup</CardTitle>");
        source.Should().Contain("<Button Width=\"Width.IsFull\">View Event</Button>");
        source.Should().Contain("To enable RTL support in shadcn/ui, see the RTL configuration guide.");
        source.Should().Contain("<CardTitle>تسجيل الدخول إلى حسابك</CardTitle>");
        source.Should().Contain("<Button Variant=\"ButtonVariant.Link\">إنشاء حساب</Button>");
        source.Should().Contain("<Button Width=\"Width.IsFull\">تسجيل الدخول</Button>");
    }

    private static string GetSuiteRootForCard()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
