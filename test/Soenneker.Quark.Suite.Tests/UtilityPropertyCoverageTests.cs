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
    public void Every_general_builder_has_component_interface_preset_and_theme_support()
    {
        // These are composition infrastructure, legacy text breaking, or component-specific sizes/variants.
        string[] excluded = ["TextBreakBuilder", "ColorPaletteBuilder", "VariantBuilder", "ButtonSizeBuilder", "CheckSizeBuilder",
            "InputSizeBuilder", "ListVariantBuilder", "PaginationSizeBuilder", "RadioSizeBuilder", "SelectSizeBuilder",
            "SliderSizeBuilder", "SwitchSizeBuilder", "ToggleSizeBuilder"];
        var properties = Properties;
        var builders = typeof(ICssBuilder).Assembly.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && !t.ContainsGenericParameters &&
            typeof(ICssBuilder).IsAssignableFrom(t) && !excluded.Contains(t.Name));
        foreach (Type builder in builders)
            properties.Should().Contain(p => Nullable.GetUnderlyingType(p.PropertyType)!.GenericTypeArguments[0] == builder, builder.Name);
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
            classes.Should().Contain("probe-" + property.Name);
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
