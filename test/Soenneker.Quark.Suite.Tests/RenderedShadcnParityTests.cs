using System;
using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Composition_searchable_select_keeps_duplicate_labels_and_empty_values_distinct()
    {
        string? selected = null;
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("first", "Same label"), new SelectOption("second", "Same label"), new SelectOption("", "None")])
            .Add(c => c.Value, "first")
            .Add(c => c.ValueChanged, value => selected = value));

        cut.Find("input").GetAttribute("value").Should().Be("Same label");
        await cut.Find("input").FocusAsync();
        await cut.Find("[data-value='option:second']").ClickAsync();
        selected.Should().Be("second");
        cut.Find("input").GetAttribute("value").Should().Be("Same label");

        await cut.Find("input").FocusAsync();
        await cut.Find("[data-value='option:']").ClickAsync();
        selected.Should().BeEmpty();
        cut.Find("input").GetAttribute("value").Should().Be("None");
    }

    [Test]
    public async Task Composition_searchable_select_filters_labels_and_rejects_disabled_choices()
    {
        string? selected = null;
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("a", "Alpha"), new SelectOption("b", "Beta", true)])
            .Add(c => c.ValueChanged, value => selected = value));

        await cut.Find("input").FocusAsync();
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "Beta" });
        cut.FindAll("[data-value='option:a']").Should().BeEmpty();
        await cut.Find("[data-value='option:b']").ClickAsync();
        selected.Should().BeNull();
    }

    [Test]
    public async Task Composition_event_timeline_orders_events_formats_timestamps_and_switches_layout()
    {
        var later = new DateTimeOffset(2026, 9, 14, 12, 0, 0, TimeSpan.Zero);
        var cut = Render<EventTimeline>(p => p
            .Add(c => c.Items, [
                new EventTimelineItem { Label = "Later", When = later },
                new EventTimelineItem { Label = "Earlier", When = later.AddHours(-1) },
                new EventTimelineItem { Label = "", When = later.AddHours(-2) }])
            .Add(c => c.DenseTimelineThreshold, 1)
            .Add(c => c.FormatTimestamp, value => value.ToString("HH:mm")));

        cut.FindAll("time").Select(t => t.TextContent.Trim()).Should().Equal("11:00", "12:00");
        cut.FindAll("div.absolute[aria-hidden='true']").Count.Should().Be(1);
        cut.Find(".overflow-x-auto").ClassList.Should().Contain("overflow-y-hidden");
        cut.FindAll("button")[0].GetAttribute("aria-pressed").Should().Be("true");
        await cut.FindAll("button")[1].ClickAsync();
        cut.FindAll("button")[1].GetAttribute("aria-pressed").Should().Be("true");
        cut.FindAll("time")[0].ParentElement!.ParentElement!.ClassList.Should().Contain("basis-[12rem]");
    }

    [Test]
    public void Composition_event_timeline_respects_explicit_tone_even_for_created_labels()
    {
        var cut = Render<EventTimeline>(p => p.Add(c => c.Items,
            [new EventTimelineItem { Label = "Created", When = DateTimeOffset.UtcNow, Tone = SemanticTone.Danger }]));

        cut.FindAll("span").Single(s => s.TextContent == "Created").ClassName.Should().Contain("--destructive");
    }

    [Test]
    public void Composition_details_only_offer_copy_when_requested()
    {
        var plain = Render<DetailsItem>(p => p.Add(c => c.Label, "Name").Add(c => c.Content, "Example"));
        plain.FindComponents<CopyableText>().Should().BeEmpty();
        plain.Markup.Should().Contain("Example");

        var copyable = Render<DetailsItem>(p => p.Add(c => c.Content, "Example").Add(c => c.Copyable, true));
        copyable.FindComponents<CopyableText>().Should().ContainSingle();
    }

    [Test]
    public void Composition_code_viewer_formats_json_and_preserves_invalid_input()
    {
        var formatted = Render<CodeViewer>(p => p.Add(c => c.Text, "{\"count\":1}"));
        formatted.FindComponent<CodeEditor>().Instance.Text.Should().Contain("\n");
        formatted.FindComponent<CodeEditor>().Instance.ReadOnly.Should().BeTrue();

        var invalid = Render<CodeViewer>(p => p.Add(c => c.Text, "{broken"));
        invalid.FindComponent<CodeEditor>().Instance.Text.Should().Be("{broken");

        var unformatted = Render<CodeViewer>(p => p.Add(c => c.Text, "{\"count\":1}").Add(c => c.FormatJson, false));
        unformatted.FindComponent<CodeEditor>().Instance.Text.Should().Be("{\"count\":1}");
    }

    [Test]
    public void Composition_chart_card_prioritizes_loading_then_error_then_empty()
    {
        RenderFragment loading = b => b.AddContent(0, "Loading slot");
        RenderFragment error = b => b.AddContent(0, "Error slot");
        RenderFragment empty = b => b.AddContent(0, "Empty slot");
        var cut = Render<ChartCard>(p => p.Add(c => c.IsLoading, true).Add(c => c.ErrorMessage, "Failure")
            .Add(c => c.IsEmpty, true).Add(c => c.LoadingContent, loading).Add(c => c.ErrorContent, error).Add(c => c.EmptyContent, empty));
        cut.Markup.Should().Contain("Loading slot").And.NotContain("Error slot").And.NotContain("Empty slot");
    }

    [Test]
    public void Composition_empty_state_omits_absent_slots_and_renders_custom_actions()
    {
        var empty = Render<EmptyState>(p => p.Add(c => c.Title, "Nothing here"));
        empty.FindAll("[data-slot='empty-content']").Should().BeEmpty();
        empty.FindAll("[data-slot='empty-media']").Should().BeEmpty();
        var action = Render<EmptyState>(p => p.Add(c => c.Actions, b => b.AddContent(0, "Retry")));
        action.Find("[data-slot='empty-content']").TextContent.Should().Be("Retry");
    }

    [Test]
    public void Composition_copy_field_accepts_presets_and_retains_accessibility_and_empty_state()
    {
        var preset = new QuarkPresetToken("test-copy-field", c => c.Width = Width.IsFull);
        var cut = Render<CopyField>(p => p.Add(c => c.Preset, preset).Add(c => c.AriaLabel, "Reference")
            .Add(c => c.Placeholder, "Not issued"));
        cut.Find("[data-slot='copy-field']").GetAttribute("aria-label").Should().Be("Reference");
        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
        cut.Markup.Should().Contain("Not issued");
    }

    [Test]
    public void Composition_badge_tone_does_not_override_explicit_preset_colors()
    {
        var preset = new QuarkPresetToken("test-tone", c => c.TextColor = TextColor.Primary);
        var cut = Render<Badge>(p => p.Add(c => c.Tone, SemanticTone.Danger).Add(c => c.Preset, preset));
        var badge = cut.Find("[data-slot='badge']");
        badge.ClassList.Should().Contain("text-primary").And.NotContain("text-destructive");
        badge.GetAttribute("data-tone").Should().Be("danger");
    }
}
