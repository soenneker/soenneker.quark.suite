using System;
using System.Linq;
using System.Reflection;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class UtilityPropertyCoverageTests : BunitContext
{
    public UtilityPropertyCoverageTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    private static PropertyInfo[] Properties => typeof(Component).GetProperties()
        .Where(p => Nullable.GetUnderlyingType(p.PropertyType) is { IsGenericType: true } type && type.GetGenericTypeDefinition() == typeof(CssValue<>)).ToArray();

    [Test]
    public void Base_utilities_have_interface_preset_and_theme_support()
    {
        var properties = Properties;
        foreach (PropertyInfo property in properties)
        {
            typeof(IComponent).GetProperty(property.Name)!.PropertyType.Should().Be(property.PropertyType, property.Name);
            typeof(QuarkPresetContext).GetProperty(property.Name)!.PropertyType.Should().Be(property.PropertyType, property.Name);
            var theme = typeof(ComponentOptions).GetProperty(property.Name);
            theme.Should().NotBeNull(property.Name);
            Nullable.GetUnderlyingType(theme!.PropertyType)!.GenericTypeArguments[0].Should().Be(
                Nullable.GetUnderlyingType(property.PropertyType)!.GenericTypeArguments[0], property.Name);
        }
    }

    [Test]
    public void Every_preset_slot_survives_freezing_renders_and_clears_when_replaced()
    {
        PropertyInfo[] properties = typeof(QuarkPresetContext).GetProperties().Where(p => p.PropertyType.IsGenericType).ToArray();
        var token = QuarkPresetToken.Freeze("all-utilities", context =>
        {
            foreach (PropertyInfo property in properties)
            {
                Type type = Nullable.GetUnderlyingType(property.PropertyType)!;
                property.SetValue(context, type.GetMethod("Raw", BindingFlags.Static | BindingFlags.Public)!.Invoke(null, ["probe-" + property.Name]));
            }
        });
        var cut = Render<Div>(p => p.Add(c => c.Preset, token));
        string[] classes = cut.Find("div").GetAttribute("class")!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (PropertyInfo property in properties)
        {
            if (typeof(Component).GetProperty(property.Name) is not null)
                classes.Should().Contain("probe-" + property.Name);
            else
                classes.Should().NotContain("probe-" + property.Name);
        }
        cut.Render(p => p.Add(c => c.Preset, QuarkPresetToken.Freeze("empty", _ => { })));
        cut.Find("div").GetAttribute("class").Should().BeNullOrEmpty();
    }

    [Test]
    public void Explicit_values_override_high_preset_slots_and_rerender()
    {
        var token = QuarkPresetToken.Freeze("utilities", c =>
        {
            c.OverflowWrap = Quark.OverflowWrap.Anywhere;
            c.OutlineStyle = Quark.OutlineStyle.Solid;
            c.Zoom = Quark.Zoom.Is125;
        });
        var cut = Render<Div>(p => p.Add(c => c.Preset, token)
            .Add(c => c.OverflowWrap, Quark.OverflowWrap.Normal)
            .Add(c => c.Zoom, default(CssValue<ZoomBuilder>)));
        var classes = cut.Find("div").GetAttribute("class")!;
        classes.Should().Contain("wrap-normal").And.NotContain("wrap-anywhere").And.NotContain("zoom-125").And.Contain("outline-solid");
        cut.Render(p => p.Add(c => c.Preset, token).Add(c => c.OverflowWrap, Quark.OverflowWrap.BreakWord).Add(c => c.Zoom, Quark.Zoom.Is125));
        cut.Find("div").GetAttribute("class").Should().Contain("wrap-break-word").And.Contain("zoom-125");
    }

    [Test]
    public void Specialized_utilities_are_owned_by_their_consumers()
    {
        (Type Owner, string[] Names)[] groups =
        [
            (typeof(Image), ["ObjectFit", "ObjectPosition"]),
            (typeof(Video), ["ObjectFit", "ObjectPosition"]),
            (typeof(Svg), ["Fill", "FillRule", "Stroke", "StrokeLineCap", "StrokeLineJoin", "SvgStrokeWidth"]),
            (typeof(Table), ["BorderCollapse", "BorderSpacing", "TableLayout"]),
            (typeof(TableCaption), ["CaptionSide"]),
            (typeof(OrderedList), ["ListStyleImage", "ListStylePosition", "ListStyleType"]),
            (typeof(UnorderedList), ["ListStyleImage", "ListStylePosition", "ListStyleType"]),
            (typeof(Grid), ["GridColumns", "GridRows", "AutoCols", "AutoRows", "GridAutoFlow"]),
            (typeof(FormControlElementBase), ["AccentColor", "NativeAppearance"]),
            (typeof(Input), ["CaretColor", "FieldSizing"]),
            (typeof(MemoInput), ["CaretColor", "FieldSizing"]),
            (typeof(TextArea), ["CaretColor", "FieldSizing"])
        ];
        foreach (var (owner, names) in groups)
        foreach (string name in names)
        {
            typeof(Component).GetProperty(name).Should().BeNull(name);
            typeof(IComponent).GetProperty(name).Should().BeNull(name);
            owner.GetProperty(name).Should().NotBeNull(owner.Name + "." + name);
        }
    }

    [Test]
    public void Specialized_presets_render_override_and_clear_on_their_owner()
    {
        var preset = QuarkPresetToken.Freeze("specialized", c =>
        {
            c.GridAutoFlow = CssValue<GridAutoFlowBuilder>.Raw("grid-flow-col");
            c.Fill = CssValue<FillBuilder>.Raw("fill-current");
            c.ListStyleType = CssValue<ListStyleTypeBuilder>.Raw("list-square");
        });
        var grid = Render<Grid>(p => p.Add(c => c.Preset, preset));
        grid.Find("div").GetAttribute("class").Should().Contain("grid-flow-col").And.NotContain("fill-current");
        grid.Render(p => p.Add(c => c.Preset, preset).Add(c => c.GridAutoFlow, CssValue<GridAutoFlowBuilder>.Raw("grid-flow-row")));
        grid.Find("div").GetAttribute("class").Should().Contain("grid-flow-row").And.NotContain("grid-flow-col");
        grid.Render(p => p.Add(c => c.Preset, preset).Add(c => c.GridAutoFlow, (CssValue<GridAutoFlowBuilder>?)null));
        grid.Find("div").GetAttribute("class").Should().NotContain("grid-flow-col").And.NotContain("grid-flow-row");
        var svg = Render<Svg>(p => p.Add(c => c.Preset, preset));
        svg.Find("svg").GetAttribute("class").Should().Contain("fill-current").And.NotContain("grid-flow-col");
        var list = Render<UnorderedList>(p => p.Add(c => c.Preset, preset));
        list.Find("ul").GetAttribute("class")!.Split(' ').Count(c => c == "list-square").Should().Be(1);
        var orderedList = Render<OrderedList>(p => p.Add(c => c.Preset, preset));
        orderedList.Find("ol").GetAttribute("class")!.Split(' ').Count(c => c == "list-square").Should().Be(1);
        list.Render(p => p.Add(c => c.Preset, QuarkPresetToken.Freeze("empty", _ => { })));
        (list.Find("ul").GetAttribute("class") ?? "").Should().NotContain("list-square");
    }

    [Test]
    public void Optional_utilities_allocate_only_when_set_and_clear_without_stale_classes()
    {
        var cut = Render<Div>(p => p.Add(c => c.MaskImage, (CssValue<MaskImageBuilder>?)null));
        string[] fields = ["_maskUtilities", "_transform3DUtilities", "_advancedTypographyUtilities"];
        foreach (string name in fields)
            typeof(Component).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(cut.Instance).Should().BeNull();

        cut.Render(p => p.Add(c => c.MaskImage, CssValue<MaskImageBuilder>.Raw("mask-none"))
            .Add(c => c.Perspective, CssValue<PerspectiveBuilder>.Raw("perspective-none"))
            .Add(c => c.FontSmoothing, CssValue<FontSmoothingBuilder>.Raw("antialiased")));
        cut.Find("div").GetAttribute("class").Should().Contain("mask-none").And.Contain("perspective-none").And.Contain("antialiased");
        cut.Render(p => p.Add(c => c.MaskImage, (CssValue<MaskImageBuilder>?)null)
            .Add(c => c.Perspective, (CssValue<PerspectiveBuilder>?)null)
            .Add(c => c.FontSmoothing, (CssValue<FontSmoothingBuilder>?)null));
        cut.Find("div").GetAttribute("class").Should().BeNullOrEmpty();
    }

    [Test]
    public void Components_do_not_hide_inherited_parameters()
    {
        foreach (Type type in typeof(Component).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Component))))
        {
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (!property.IsDefined(typeof(ParameterAttribute)) || Nullable.GetUnderlyingType(property.PropertyType) is not { IsGenericType: true } valueType || valueType.GetGenericTypeDefinition() != typeof(CssValue<>)) continue;
                PropertyInfo? parent = type.BaseType?.GetProperty(property.Name);
                if (parent is null) continue;
                (property.GetMethod!.GetBaseDefinition() == parent.GetMethod!.GetBaseDefinition()).Should().BeTrue(type.Name + "." + property.Name);
            }
        }
    }

    [Test]
    public void Newly_exposed_theme_utilities_generate_css_declarations()
    {
        string css = ComponentCssGenerator.Generate(new ComponentOptions
        {
            OverflowWrap = Quark.OverflowWrap.Anywhere,
            WordBreak = Quark.WordBreak.Keep,
            FlexBasis = Quark.FlexBasis.Is1of2,
            Order = Quark.Order.Is1,
            OutlineOffset = Quark.OutlineOffset.Token("4"),
            NativeAppearance = Quark.Appearance.None,
            ScrollBehavior = Quark.ScrollBehavior.Smooth,
            BackgroundSize = Quark.BackgroundSize.Cover,
            Rotate = Quark.Rotate.Token("45"),
            GridColumns = Quark.GridCols.Token("3")
        });
        css.Should().Contain("overflow-wrap: anywhere;").And.Contain("word-break: keep-all;")
            .And.Contain("flex-basis: 50%;").And.Contain("order: 1;").And.Contain("outline-offset: 4px;")
            .And.Contain("appearance: none;").And.Contain("scroll-behavior: smooth;")
            .And.Contain("background-size: cover;").And.Contain("rotate: 45deg;")
            .And.Contain("grid-template-columns: repeat(3, minmax(0, 1fr));");
    }
}
