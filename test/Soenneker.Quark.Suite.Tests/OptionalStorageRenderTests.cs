using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class OptionalStorageRenderTests : BunitContext
{
    public OptionalStorageRenderTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Every_typed_style_slot_preserves_values_clearing_and_instance_isolation()
    {
        var first = new Div();
        var second = new Div();
        var properties = typeof(Component).GetProperties().Where(p =>
            Nullable.GetUnderlyingType(p.PropertyType) is { IsGenericType: true } type &&
            type.GetGenericTypeDefinition() == typeof(CssValue<>)).ToArray();
        properties.Length.Should().BeGreaterThan(100);
        foreach (var property in properties)
        {
            Type valueType = Nullable.GetUnderlyingType(property.PropertyType)!;
            object value = valueType.GetMethod("Raw", BindingFlags.Static | BindingFlags.Public)!.Invoke(null, ["probe-" + property.Name])!;
            property.SetValue(first, value);
            property.GetValue(first).Should().Be(value, property.Name);
            property.GetValue(second).Should().BeNull(property.Name);
            property.SetValue(first, null);
            property.GetValue(first).Should().BeNull(property.Name);
            property.SetValue(first, value);
            property.GetValue(first).Should().Be(value, property.Name);
        }
    }

    [Test]
    public void Every_optional_html_parameter_matches_on_normal_and_cancellable_elements()
    {
        foreach (Type type in new[] { typeof(Div), typeof(Score) })
        {
            object first = Activator.CreateInstance(type)!;
            object second = Activator.CreateInstance(type)!;
            Type baseType = type == typeof(Div) ? typeof(Element) : typeof(CancellableElement);
            foreach (var property in baseType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                         .Where(p => p.IsDefined(typeof(ParameterAttribute)) && (p.PropertyType == typeof(string) || p.PropertyType == typeof(bool?))))
            {
                object value = property.PropertyType == typeof(string) ? "sample" : false;
                property.SetValue(first, value);
                property.GetValue(first).Should().Be(value, property.Name);
                property.GetValue(second).Should().BeNull(property.Name);
                property.SetValue(first, null);
                property.GetValue(first).Should().BeNull(property.Name);
            }
        }
    }

    [Test]
    public void Common_component_construction_stays_within_allocation_budget()
    {
        // Byte budgets catch eagerly reintroduced per-instance storage; no timing threshold.
        MeasureConstruction(() => new Div()).Should().BeLessThanOrEqualTo(880);
        MeasureConstruction(() => new Button()).Should().BeLessThanOrEqualTo(1200);
    }

    private static long MeasureConstruction(Func<object> create)
    {
        for (var i = 0; i < 1000; i++) GC.KeepAlive(create());
        long before = GC.GetAllocatedBytesForCurrentThread();
        for (var i = 0; i < 1000; i++) GC.KeepAlive(create());
        return (GC.GetAllocatedBytesForCurrentThread() - before) / 1000;
    }

    [Test]
    public void Optional_attributes_remain_independent_and_can_be_cleared()
    {
        var first = Render<Div>(p => p.Add(c => c.ContentEditable, "plaintext-only")
            .Add(c => c.SpellCheck, false).Add(c => c.Part, "content").Add(c => c.Inert, true)
            .Add(c => c.ItemScope, true).Add(c => c.ItemType, "https://schema.org/Thing")
            .Add(c => c.AriaLive, "polite").Add(c => c.AriaAtomic, false).Add(c => c.AriaBrailleLabel, "Message"));
        var second = Render<Div>();
        second.Find("div").HasAttribute("spellcheck").Should().BeFalse();
        second.Find("div").HasAttribute("itemscope").Should().BeFalse();
        second.Find("div").HasAttribute("aria-live").Should().BeFalse();
        var element = first.Find("div");
        element.GetAttribute("spellcheck").Should().Be("false");
        element.GetAttribute("part").Should().Be("content");
        element.HasAttribute("itemscope").Should().BeTrue();
        element.GetAttribute("aria-atomic").Should().Be("false");
        element.GetAttribute("aria-braillelabel").Should().Be("Message");
        first.Render(p => p.Add(c => c.ContentEditable, (string?)null).Add(c => c.SpellCheck, (bool?)null)
            .Add(c => c.Part, (string?)null).Add(c => c.Inert, false)
            .Add(c => c.ItemScope, (bool?)null).Add(c => c.ItemType, (string?)null)
            .Add(c => c.AriaLive, (string?)null).Add(c => c.AriaAtomic, (bool?)null).Add(c => c.AriaBrailleLabel, (string?)null));
        foreach (var name in new[] { "contenteditable", "spellcheck", "part", "inert", "itemscope", "itemtype", "aria-live", "aria-atomic", "aria-braillelabel" })
            first.Find("div").HasAttribute(name).Should().BeFalse();
    }

    [Test]
    public void Logical_layout_values_preserve_preset_defaults_and_explicit_null_overrides()
    {
        var preset = new QuarkPresetToken("logical-layout", c => { c.InlineSize = "inline-size-10"; c.InsetStart = "start-2"; });
        var cut = Render<Div>(p => p.Add(c => c.Preset, preset)
            .Add(c => c.InlineSize, (CssValue<InlineSizeBuilder>?)"inline-size-20")
            .Add(c => c.InsetStart, (CssValue<InsetStartBuilder>?)"start-4"));
        cut.Find("div").ClassList.Should().Contain("inline-size-20").And.Contain("start-4");
        cut.Render(p => p.Add(c => c.InlineSize, (CssValue<InlineSizeBuilder>?)null)
            .Add(c => c.InsetStart, (CssValue<InsetStartBuilder>?)null));
        cut.Find("div").ClassList.Should().NotContain("inline-size-10").And.NotContain("start-2");
        var defaults = Render<Div>(p => p.Add(c => c.Preset, preset));
        defaults.Find("div").ClassList.Should().Contain("inline-size-10").And.Contain("start-2");
        cut.Find("div").ClassList.Should().NotContain("inline-size-20").And.NotContain("start-4");
    }
}
