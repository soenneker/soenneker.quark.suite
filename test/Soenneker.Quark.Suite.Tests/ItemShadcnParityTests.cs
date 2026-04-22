using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Item_and_text_slots_match_shadcn_base_classes()
    {
        var item = Render<Item>(parameters => parameters
            .Add(p => p.Variant, ItemVariant.Outline)
            .Add(p => p.ChildContent, "Item"));
        var itemXs = Render<Item>(parameters => parameters
            .Add(p => p.Variant, ItemVariant.Outline)
            .Add(p => p.Size, ItemSize.ExtraSmall)
            .Add(p => p.ChildContent, "Item"));

        var itemClasses = item.Find("[data-slot='item']").GetAttribute("class")!;
        var itemXsClasses = itemXs.Find("[data-slot='item']").GetAttribute("class")!;

        itemClasses.Should().Contain("group/item");
        itemClasses.Should().Contain("duration-100");
        itemClasses.Should().Contain("focus-visible:ring-[3px]");
        itemClasses.Should().Contain("[a]:hover:bg-muted");
        itemClasses.Should().Contain("border-border");
        itemClasses.Should().Contain("gap-2.5");
        itemClasses.Should().Contain("px-3");
        itemClasses.Should().Contain("py-2.5");
        itemClasses.Should().Contain("flex");
        itemClasses.Should().Contain("w-full");
        itemClasses.Should().Contain("flex-wrap");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("rounded-lg");
        itemClasses.Should().Contain("border");
        itemClasses.Should().Contain("text-sm");
        itemClasses.Should().Contain("transition-colors");
        itemClasses.Should().NotContain("border-1");
        itemClasses.Should().NotContain("q-item");

        itemXsClasses.Should().Contain("gap-2");
        itemXsClasses.Should().Contain("px-2.5");
        itemXsClasses.Should().Contain("py-2");
        itemXsClasses.Should().Contain("in-data-[slot=dropdown-menu-content]:p-0");
        itemXsClasses.Should().Contain("border");
        itemXsClasses.Should().NotContain("border-1");
    }
}
