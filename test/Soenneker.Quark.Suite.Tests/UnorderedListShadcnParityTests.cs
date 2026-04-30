using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void UnorderedList_matches_shadcn_typography_list_contract()
    {
        var cut = Render<UnorderedList>(parameters => parameters.AddChildContent<UnorderedListItem>(item => item.Add(p => p.ChildContent, "Item")));

        var list = cut.Find("[data-slot='unordered-list']");
        var item = cut.Find("[data-slot='unordered-list-item']");
        var classes = list.GetAttribute("class")!;

        list.TagName.Should().Be("UL");
        item.TagName.Should().Be("LI");
        classes.Should().ContainAll("my-6", "ms-6", "list-disc", "[&>li]:mt-2");
        classes.Should().NotContain("ml-6");
        classes.Should().NotContain("q-unordered-list");
    }

    [Test]
    public void UnorderedList_supports_typed_list_style_and_unstyled_variant()
    {
        var square = Render<UnorderedList>(parameters => parameters
            .Add(p => p.ListStyleType, (CssValue<ListStyleTypeBuilder>) ListStyleType.Square)
            .AddChildContent<UnorderedListItem>(item => item.Add(p => p.ChildContent, "Item")));
        var unstyled = Render<UnorderedList>(parameters => parameters
            .Add(p => p.ListVariant, ListVariant.None)
            .AddChildContent<UnorderedListItem>(item => item.Add(p => p.ChildContent, "Item")));

        square.Find("[data-slot='unordered-list']").GetAttribute("style")!.Should().Contain("list-style-type: square");
        unstyled.Find("[data-slot='unordered-list']").GetAttribute("class")!.Should().Contain("list-none");
        unstyled.Find("[data-slot='unordered-list']").GetAttribute("class")!.Should().NotContain("list-disc");
    }
}
