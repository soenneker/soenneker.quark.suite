using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class StyleEmissionTests : BunitContext
{
    public StyleEmissionTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Every_style_emits_from_parameters_and_presets_and_respects_explicit_null()
    {
        var properties = typeof(Component).GetProperties().Where(p =>
            Nullable.GetUnderlyingType(p.PropertyType) is { IsGenericType: true } type &&
            type.GetGenericTypeDefinition() == typeof(CssValue<>)).ToArray();
        properties.Length.Should().Be(197);
        foreach (var property in properties)
        {
            string token = "probe-" + property.Name;
            Type type = Nullable.GetUnderlyingType(property.PropertyType)!;
            object value = type.GetMethod("Raw", BindingFlags.Public | BindingFlags.Static)!.Invoke(null, [token])!;
            var cut = Render<Div>();
            cut.Render(ParameterView.FromDictionary(new Dictionary<string, object?> { [property.Name] = value }));
            cut.Find("div").ClassList.Should().Contain(token, property.Name);
            cut.Render(ParameterView.FromDictionary(new Dictionary<string, object?> { [property.Name] = null }));
            cut.Find("div").ClassList.Should().NotContain(token, property.Name);

            var presetProperty = typeof(QuarkPresetContext).GetProperty(property.Name)!;
            var preset = new QuarkPresetToken("probe", context => presetProperty.SetValue(context, value));
            var presetCut = Render<Div>(p => p.Add(c => c.Preset, preset));
            presetCut.Find("div").ClassList.Should().Contain(token, property.Name);
            presetCut.Render(ParameterView.FromDictionary(new Dictionary<string, object?>
            {
                [nameof(Component.Preset)] = preset,
                [property.Name] = null
            }));
            presetCut.Find("div").ClassList.Should().NotContain(token, property.Name);

            // An empty preset suppresses a component default; a null preset slot falls back
            // to that default. These are different from an explicitly supplied null parameter.
            object? presetValue = type.GetMethod("Raw", BindingFlags.Public | BindingFlags.Static)!.Invoke(null, [string.Empty]);
            var emptyPreset = new QuarkPresetToken("empty", context => presetProperty.SetValue(context, presetValue));
            var defaultCut = Render<Div>();
            property.SetValue(defaultCut.Instance, value);
            defaultCut.Render(p => p.Add(c => c.Preset, emptyPreset));
            defaultCut.Find("div").ClassList.Should().NotContain(token, property.Name);
            presetValue = null;
            defaultCut.Render(p => p.Add(c => c.Preset, emptyPreset));
            defaultCut.Find("div").ClassList.Should().Contain(token, property.Name);
        }
    }
}
