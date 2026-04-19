using System;
using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class BuilderRuntimeContractTests : BunitContext
{
    public BuilderRuntimeContractTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Fact]
    public void Cursor_default_maps_to_cursor_default()
    {
        Assert.Equal("cursor-default", Cursor.Default.ToClass());
    }

    [Fact]
    public void Overflow_axis_builders_emit_axis_specific_classes()
    {
        Assert.Equal("overflow-x-hidden", Overflow.X.Hidden.ToClass());
        Assert.Equal("overflow-y-auto", Overflow.Y.Auto.ToClass());
    }

    [Fact]
    public void Padding_builder_supports_axis_specific_arbitrary_spacing_tokens()
    {
        Assert.Equal("px-2 py-1.5", Padding.Is2.OnX.Token("1.5").OnY.ToClass());
    }

    [Fact]
    public void Physical_side_builders_use_left_and_right_tokens_instead_of_inline_start_end()
    {
        Assert.Equal("pr-8 pl-2 py-1.5", Padding.Token("8").FromRight.Is2.FromLeft.Token("1.5").OnY.ToClass());
        Assert.Equal("border", Border.Is1.ToClass());
        Assert.Equal("border-b", Border.Is1.FromBottom.ToClass());
        Assert.Equal("border-l-4", Border.Is4.FromLeft.ToClass());
        Assert.Equal("mr-3", Margin.Is3.FromRight.ToClass());
        Assert.Equal("-mx-1", Margin.Token("-1").OnX.ToClass());
        Assert.Equal("mt-1.5", Margin.Token("1.5").FromTop.ToClass());
    }

    [Fact]
    public void MaxHeight_accepts_height_builder_token_and_emits_max_height_utility()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.MaxHeight, Height.Token("72")));

        var box = cut.Find("[data-slot='test-box']");
        string classes = box.GetAttribute("class")!;
        string? style = box.GetAttribute("style");
        string[] classTokens = classes.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        classTokens.Should().Contain("max-h-72");
        classTokens.Should().NotContain("h-72");
        style.Should().BeNull();
    }

    [Fact]
    public void Component_cursor_and_axis_overflow_properties_use_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Cursor, Cursor.Default)
            .Add(p => p.OverflowX, Overflow.X.Hidden)
            .Add(p => p.OverflowY, Overflow.Y.Auto)
            .Add(p => p.Padding, Padding.Is2.OnX.Token("1.5").OnY));

        var box = cut.Find("[data-slot='test-box']");
        string classes = box.GetAttribute("class")!;

        classes.Should().Contain("cursor-default");
        classes.Should().Contain("overflow-x-hidden");
        classes.Should().Contain("overflow-y-auto");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-1.5");
    }

    private sealed class TestRenderBox : Element
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, BuildAttributes());
            builder.CloseElement();
        }

        protected override void BuildAttributesCore(Dictionary<string, object> attributes)
        {
            base.BuildAttributesCore(attributes);
            attributes["data-slot"] = "test-box";
        }
    }
}
