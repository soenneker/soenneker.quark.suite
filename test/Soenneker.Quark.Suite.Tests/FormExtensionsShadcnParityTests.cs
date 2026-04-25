using System;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void MemoInput_reuses_shadcn_textarea_contract()
    {
        var cut = Render<MemoInput>(parameters => parameters
            .Add(p => p.Placeholder, "Message")
            .Add(p => p.Rows, 5)
            .Add(p => p.MaxLength, 120));

        var textarea = cut.Find("[data-slot='textarea']");
        var classes = textarea.GetAttribute("class")!;

        classes.Should().Contain("min-h-16");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("aria-invalid:border-destructive");
        textarea.GetAttribute("rows").Should().Be("5");
        textarea.GetAttribute("maxlength").Should().Be("120");
        textarea.GetAttribute("placeholder").Should().Be("Message");
    }

    [Test]
    public void DateInput_formats_native_input_values_and_forwards_consumer_attributes()
    {
        var cut = Render<DateInput>(parameters => parameters
            .Add(p => p.DateOnly, new DateOnly(2026, 4, 24))
            .Add(p => p.InputMode, DateInputMode.DateTime)
            .Add(p => p.MinDateOnly, new DateOnly(2026, 4, 1))
            .Add(p => p.MaxDateOnly, new DateOnly(2026, 4, 30))
            .AddUnmatched("data-testid", "launch-date")
            .AddUnmatched("aria-label", "Launch date"));

        var input = cut.Find("input[data-slot='input']");

        input.GetAttribute("type").Should().Be("datetime-local");
        input.GetAttribute("value").Should().Be("2026-04-24T00:00");
        input.GetAttribute("min").Should().Be("2026-04-01T00:00");
        input.GetAttribute("max").Should().Be("2026-04-30T00:00");
        input.GetAttribute("data-testid").Should().Be("launch-date");
        input.GetAttribute("aria-label").Should().Be("Launch date");
    }

    [Test]
    public void CurrencyInput_keeps_extension_root_slot_and_shadcn_input_contract()
    {
        var cut = Render<CurrencyInput>(parameters => parameters
            .Add(p => p.Value, 1234.5m)
            .Add(p => p.Symbol, "$")
            .Add(p => p.AllowNegative, false));

        var root = cut.Find("[data-slot='currency-input']");
        var input = root.QuerySelector("input[data-slot='input']")!;
        var classes = input.GetAttribute("class")!;

        root.GetAttribute("class").Should().Contain("relative");
        input.GetAttribute("value").Should().Be("1,234.50");
        input.GetAttribute("inputmode").Should().Be("decimal");
        classes.Should().Contain("text-right");
        classes.Should().Contain("tabular-nums");
        classes.Should().Contain("pl-9");
    }

    [Test]
    public void DateTimePicker_exposes_root_input_and_panel_slots()
    {
        var cut = Render<DateTimePicker>(parameters => parameters
            .Add(p => p.SelectedDateTime, new DateTime(2026, 4, 24, 13, 30, 0)));

        var root = cut.Find("[data-slot='date-time-picker']");
        var input = root.QuerySelector("input[data-slot='date-time-picker-input']")!;

        root.GetAttribute("class").Should().Contain("q-date-time-picker");
        input.GetAttribute("value").Should().Be("2026-04-24 01:30 PM");
        input.GetAttribute("aria-expanded").Should().Be("false");
        input.GetAttribute("aria-controls").Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public void CodeEditor_exposes_extension_slot_and_layout_styles()
    {
        var cut = Render<CodeEditor>(parameters => parameters
            .Add(p => p.Text, "Console.WriteLine(\"hi\");")
            .Add(p => p.Language, "csharp")
            .Add(p => p.ReadOnly, true));

        var editor = cut.Find("[data-slot='code-editor']");
        var style = editor.GetAttribute("style")!;

        style.Should().Contain("position: relative");
        style.Should().Contain("width: 100%");
        style.Should().Contain("min-width: 0");
    }
}
