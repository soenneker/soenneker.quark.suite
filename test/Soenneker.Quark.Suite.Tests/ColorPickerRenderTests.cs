using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ColorPicker_renders_inline_picker_surface()
    {
        var cut = Render<ColorPicker>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.Value, "#8b5cf6"));

        cut.Find("[data-slot='color-picker']").Should().NotBeNull();
        cut.Find("[data-slot='color-picker-canvas']").Should().NotBeNull();
        cut.Find("[data-slot='color-picker-hue']").Should().NotBeNull();
        cut.Find("[data-slot='color-picker-alpha']").Should().NotBeNull();
        cut.Find("[data-slot='color-picker-eyedropper']").Should().NotBeNull();
        cut.Find("[data-slot='color-picker-value']").GetAttribute("value").Should().Be("#8b5cf6");
    }

    [Test]
    public void ColorPicker_renders_popover_trigger()
    {
        var cut = Render<ColorPicker>(parameters => parameters
            .Add(p => p.Value, "#22c55e"));

        var button = cut.Find("button");
        button.TextContent.Should().Contain("#22c55e");
        button.InnerHtml.Should().Contain("background: rgba(34, 197, 94, 1)");
    }

    [Test]
    public async Task ColorPicker_changes_format_to_css()
    {
        string? value = null;

        var cut = Render<ColorPicker>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.Value, "#8b5cf6")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next =>
            {
                value = next;
                return Task.CompletedTask;
            })));

        await cut.Find("select[data-slot='color-picker-format']").ChangeAsync(new ChangeEventArgs { Value = ColorPickerFormat.Css.ToString() });

        value.Should().StartWith("rgba(");
    }

    [Test]
    public async Task ColorPicker_selects_preset()
    {
        string? value = null;

        var cut = Render<ColorPicker>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.ShowPresets, true)
            .Add(p => p.Value, "#8b5cf6")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next =>
            {
                value = next;
                return Task.CompletedTask;
            })));

        await cut.Find("[aria-label='Select #ef4444']").ClickAsync(new MouseEventArgs());

        value.Should().Be("#ef4444");
    }

    [Test]
    public void ColorPicker_can_hide_each_optional_surface()
    {
        var cut = Render<ColorPicker>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.ShowCanvas, false)
            .Add(p => p.ShowHue, false)
            .Add(p => p.ShowAlpha, false)
            .Add(p => p.ShowAlphaSlider, false)
            .Add(p => p.ShowEyedropper, false)
            .Add(p => p.ShowFormatSelector, false)
            .Add(p => p.ShowValueInput, false)
            .Add(p => p.ShowPresets, false));

        cut.FindAll("[data-slot='color-picker-canvas']").Should().BeEmpty();
        cut.FindAll("[data-slot='color-picker-hue']").Should().BeEmpty();
        cut.FindAll("[data-slot='color-picker-alpha']").Should().BeEmpty();
        cut.FindAll("[data-slot='color-picker-eyedropper']").Should().BeEmpty();
        cut.FindAll("[data-slot='color-picker-format']").Should().BeEmpty();
        cut.FindAll("[data-slot='color-picker-value']").Should().BeEmpty();
        cut.FindAll("[data-slot='color-picker-presets']").Should().BeEmpty();
    }

    [Test]
    public async Task ColorPicker_changes_alpha_from_percentage_input()
    {
        string? value = null;
        var cut = Render<ColorPicker>(parameters => parameters
            .Add(p => p.Inline, true)
            .Add(p => p.Format, ColorPickerFormat.Css)
            .Add(p => p.Value, "rgba(99, 102, 241, 1)")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, next => value = next)));

        await cut.Find("[data-slot='color-picker-alpha-input'] input").ChangeAsync(new ChangeEventArgs { Value = "35" });

        value.Should().EndWith(", 0.35)");
    }
}
