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
        itemClasses.Should().Contain("[a]:transition-colors");
        itemClasses.Should().Contain("[a]:hover:bg-accent/50");
        itemClasses.Should().Contain("border-border");
        itemClasses.Should().Contain("gap-4");
        itemClasses.Should().Contain("p-4");
        itemClasses.Should().Contain("flex");
        itemClasses.Should().Contain("flex-wrap");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("rounded-md");
        itemClasses.Should().Contain("border");
        itemClasses.Should().Contain("text-sm");
        itemClasses.Should().Contain("transition-colors");
        itemClasses.Should().NotContain("[a]:hover:bg-muted");
        itemClasses.Should().NotContain("rounded-lg");
        itemClasses.Should().NotContain("w-full");
        itemClasses.Should().NotContain("border-1");
        itemClasses.Should().NotContain("q-item");

        itemXsClasses.Should().Contain("gap-2.5");
        itemXsClasses.Should().Contain("px-4");
        itemXsClasses.Should().Contain("py-3");
        itemXsClasses.Should().NotContain("in-data-[slot=dropdown-menu-content]:p-0");
        itemXs.Find("[data-slot='item']").GetAttribute("data-size").Should().Be("sm");
        itemXsClasses.Should().Contain("border");
        itemXsClasses.Should().NotContain("border-1");
    }

    [Test]
    public void Item_sm_size_and_muted_variant_match_shadcn_contract()
    {
        var cut = Render<Item>(parameters => parameters
            .Add(p => p.Variant, ItemVariant.Muted)
            .Add(p => p.Size, ItemSize.Small)
            .Add(p => p.ChildContent, "Item"));

        var item = cut.Find("[data-slot='item']");
        var classes = item.GetAttribute("class")!;

        item.GetAttribute("data-variant").Should().Be("muted");
        item.GetAttribute("data-size").Should().Be("sm");
        classes.Should().Contain("bg-muted/50");
        classes.Should().Contain("gap-2.5");
        classes.Should().Contain("px-4");
        classes.Should().Contain("py-3");
        classes.Should().NotContain("px-3");
        classes.Should().NotContain("py-2.5");
    }

    [Test]
    public void Item_compound_slots_match_shadcn_contract()
    {
        var media = Render<ItemMedia>(parameters => parameters
            .Add(p => p.Variant, ItemMediaVariant.Icon)
            .Add(p => p.ChildContent, "I"));
        var content = Render<ItemContent>(parameters => parameters.Add(p => p.ChildContent, "Content"));
        var actions = Render<ItemActions>(parameters => parameters.Add(p => p.ChildContent, "Actions"));
        var header = Render<ItemHeader>(parameters => parameters.Add(p => p.ChildContent, "Header"));
        var footer = Render<ItemFooter>(parameters => parameters.Add(p => p.ChildContent, "Footer"));

        var mediaClasses = media.Find("[data-slot='item-media']").GetAttribute("class")!;
        var contentClasses = content.Find("[data-slot='item-content']").GetAttribute("class")!;
        var actionsClasses = actions.Find("[data-slot='item-actions']").GetAttribute("class")!;
        var headerClasses = header.Find("[data-slot='item-header']").GetAttribute("class")!;
        var footerClasses = footer.Find("[data-slot='item-footer']").GetAttribute("class")!;

        mediaClasses.Should().Contain("flex");
        mediaClasses.Should().Contain("shrink-0");
        mediaClasses.Should().Contain("items-center");
        mediaClasses.Should().Contain("justify-center");
        mediaClasses.Should().Contain("gap-2");
        mediaClasses.Should().Contain("group-has-[[data-slot=item-description]]/item:translate-y-0.5");
        mediaClasses.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
        mediaClasses.Should().Contain("size-8");
        mediaClasses.Should().Contain("rounded-sm");
        mediaClasses.Should().Contain("border");
        mediaClasses.Should().Contain("bg-muted");
        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("flex-1");
        contentClasses.Should().Contain("flex-col");
        contentClasses.Should().Contain("gap-1");
        contentClasses.Should().Contain("[&+[data-slot=item-content]]:flex-none");
        actionsClasses.Should().Contain("flex");
        actionsClasses.Should().Contain("items-center");
        actionsClasses.Should().Contain("gap-2");
        headerClasses.Should().Contain("basis-full");
        headerClasses.Should().Contain("justify-between");
        headerClasses.Should().NotContain("w-full");
        footerClasses.Should().Contain("basis-full");
        footerClasses.Should().Contain("justify-between");
        footerClasses.Should().NotContain("w-full");
    }

    [Test]
    public void ItemGroup_and_separator_match_shadcn_contract()
    {
        var group = Render<ItemGroup>(parameters => parameters.Add(p => p.ChildContent, "Items"));
        var separator = Render<ItemSeparator>();

        var groupElement = group.Find("[data-slot='item-group']");
        var groupClasses = groupElement.GetAttribute("class")!;
        var separatorElement = separator.Find("[data-slot='item-separator']");
        var separatorClasses = separatorElement.GetAttribute("class")!;

        groupElement.GetAttribute("role").Should().Be("list");
        groupClasses.Should().Contain("group/item-group");
        groupClasses.Should().Contain("flex");
        groupClasses.Should().Contain("flex-col");
        separatorElement.GetAttribute("data-orientation").Should().Be("horizontal");
        separatorClasses.Should().Contain("my-0");
    }
}
