using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;


namespace Soenneker.Quark.Suite.Tests;

public sealed class BuilderRuntimeContractTests : BunitContext
{
    public BuilderRuntimeContractTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Cursor_default_maps_to_cursor_default()
    {
        Cursor.Default.ToClass().Should().Be("cursor-default");
    }

    [Test]
    public void Overflow_axis_builders_emit_axis_specific_classes()
    {
        Overflow.X.Hidden.ToClass().Should().Be("overflow-x-hidden");
        Overflow.Y.Auto.ToClass().Should().Be("overflow-y-auto");
    }

    [Test]
    public void Padding_builder_supports_axis_specific_arbitrary_spacing_tokens()
    {
        Padding.Is2.OnX.Token("1.5").OnY.ToClass().Should().Be("px-2 py-1.5");
    }

    [Test]
    public void Physical_side_builders_use_left_and_right_tokens_instead_of_inline_start_end()
    {
        Padding.Token("8").FromRight.Is2.FromLeft.Token("1.5").OnY.ToClass().Should().Be("pr-8 pl-2 py-1.5");
        Border.Is1.ToClass().Should().Be("border");
        Border.Is1.FromBottom.ToClass().Should().Be("border-b");
        Border.Is4.FromLeft.ToClass().Should().Be("border-l-4");
        Margin.Is3.FromRight.ToClass().Should().Be("mr-3");
        Margin.Token("-1").OnX.ToClass().Should().Be("-mx-1");
        Margin.Token("1.5").FromTop.ToClass().Should().Be("mt-1.5");
    }

    [Test]
    public void MaxHeight_accepts_height_builder_token_and_emits_max_height_utility()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.MaxHeight, Height.Token("72")));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;
        var style = box.GetAttribute("style");
        var classTokens = classes.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        classTokens.Should().Contain("max-h-72");
        classTokens.Should().NotContain("h-72");
        style.Should().BeNull();
    }

    [Test]
    public void Component_cursor_and_axis_overflow_properties_use_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Cursor, Cursor.Default)
            .Add(p => p.OverflowX, Overflow.X.Hidden)
            .Add(p => p.OverflowY, Overflow.Y.Auto)
            .Add(p => p.Padding, Padding.Is2.OnX.Token("1.5").OnY));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("cursor-default");
        classes.Should().Contain("overflow-x-hidden");
        classes.Should().Contain("overflow-y-auto");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-1.5");
    }

    [Test]
    public void Component_shrink_property_uses_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Shrink, Shrink.Is0.OnMd.Is1));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("shrink-0");
        classes.Should().Contain("md:shrink");
    }

    [Test]
    public void Component_flex_direction_wrap_and_grow_properties_use_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.FlexDirection, FlexDirection.Col.OnMd.Row)
            .Add(p => p.FlexWrap, FlexWrap.Wrap.OnLg.NoWrap)
            .Add(p => p.Grow, Grow.Is0.OnSm.Is1));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("md:flex");
        classes.Should().Contain("md:flex-row");
        classes.Should().Contain("flex-wrap");
        classes.Should().Contain("lg:flex");
        classes.Should().Contain("lg:flex-nowrap");
        classes.Should().Contain("grow-0");
        classes.Should().Contain("sm:grow");
    }

    [Test]
    public void Class_merging_deduplicates_identical_tokens()
    {
        CssValue<FlexBuilder> flex = "flex flex-col";

        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Display, Display.Flex)
            .Add(p => p.Flex, flex)
            .Add(p => p.Class, "flex flex flex-col"));

        var box = cut.Find("[data-slot='test-box']");
        var tokens = box.GetAttribute("class")!
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        tokens.Count(token => token == "flex").Should().Be(1);
        tokens.Count(token => token == "flex-col").Should().Be(1);
    }

    [Test]
    public void Section_has_no_default_padding_class()
    {
        var cut = Render<Section>();

        var section = cut.Find("section");
        var classes = section.GetAttribute("class");

        classes.Should().BeNullOrEmpty();
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
