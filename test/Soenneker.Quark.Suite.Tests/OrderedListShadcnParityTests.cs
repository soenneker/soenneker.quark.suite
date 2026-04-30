using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void OrderedList_defaults_match_shadcn_typography_list_contract()
    {
        var cut = Render<OrderedList>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<OrderedListItem>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(child => child.AddContent(0, "First step")));
                builder.CloseComponent();
            })));

        var list = cut.Find("ol[data-slot='ordered-list']");
        var item = cut.Find("li[data-slot='ordered-list-item']");

        var classes = list.GetAttribute("class")!;

        classes.Should().ContainAll("my-6", "ms-6", "list-decimal", "[&>li]:mt-2");
        classes.Should().NotContain("ml-6");
        item.TextContent.Should().Be("First step");
    }

    [Test]
    public void OrderedList_supports_typed_list_style_and_unstyled_variants()
    {
        var lowerAlpha = Render<OrderedList>(parameters => parameters
            .Add(p => p.ListStyleType, ListStyleType.LowerAlpha)
            .Add(p => p.ChildContent, "Alpha"));

        lowerAlpha.Find("ol").GetAttribute("class").Should().ContainAll("my-6", "ms-6", "[&>li]:mt-2");
        lowerAlpha.Find("ol").GetAttribute("style").Should().Contain("list-style-type: lower-alpha");
        lowerAlpha.Find("ol").GetAttribute("class").Should().NotContain("list-decimal");

        var unstyled = Render<OrderedList>(parameters => parameters
            .Add(p => p.ListVariant, ListVariant.None)
            .Add(p => p.ChildContent, "Plain"));

        unstyled.Find("ol").GetAttribute("class").Should().ContainAll("list-none", "p-0");
        unstyled.Find("ol").GetAttribute("class").Should().NotContain("my-6");
        unstyled.Find("ol").GetAttribute("class").Should().NotContain("list-decimal");
    }
}
