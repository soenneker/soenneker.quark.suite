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

        await cut.Find("button[data-slot='color-picker-format'][data-active='false']:nth-child(3)").ClickAsync(new MouseEventArgs());

        value.Should().StartWith("rgba(");
    }

    [Test]
    public async Task ColorPicker_selects_preset()
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

        await cut.Find("[aria-label='Select #ef4444']").ClickAsync(new MouseEventArgs());

        value.Should().Be("#ef4444");
    }
}
