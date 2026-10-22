using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task EditableAutoSave_search_does_not_save_and_selection_uses_logical_value()
    {
        var saves = new List<string?>();
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("east", "Eastern"), new SelectOption("west", "Western")])
            .Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (value, _) => { saves.Add(value); return ValueTask.CompletedTask; }));
        saves.Should().BeEmpty();
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.SearchChanged.InvokeAsync("Eastern"));
        saves.Should().BeEmpty();
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChanged.InvokeAsync("option:east"));
        cut.WaitForAssertion(() => saves.Should().Equal("east"));
        cut.WaitForAssertion(() => cut.Find("[data-slot='autosave-status']").GetAttribute("data-state").Should().Be("saved"));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChanged.InvokeAsync("option:east"));
        saves.Should().ContainSingle();
    }

    [Test]
    public async Task EditableAutoSave_multiple_select_saves_logical_selection_snapshot()
    {
        IReadOnlyList<string>? saved = null;
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("a", "Alpha"), new SelectOption("b", "Beta")])
            .Add(c => c.Multiple, true).Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSaveMultiple, (values, _) => { saved = values; return ValueTask.CompletedTask; }));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChangedMultiple.InvokeAsync(["option:a", "option:b"]));
        cut.WaitForAssertion(() => saved.Should().Equal("a", "b"));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChangedMultiple.InvokeAsync([]));
        cut.WaitForAssertion(() => saved.Should().BeEmpty());
    }

    [Test]
    public async Task EditableAutoSave_failure_is_visible_and_later_selection_recovers()
    {
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("bad", "Bad"), new SelectOption("good", "Good")])
            .Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (value, _) => value == "bad"
                ? ValueTask.FromException(new InvalidOperationException("Save failed")) : ValueTask.CompletedTask));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChanged.InvokeAsync("option:bad"));
        cut.WaitForAssertion(() => cut.Find("[data-slot='autosave-status']").GetAttribute("data-state").Should().Be("failed"));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChanged.InvokeAsync("option:good"));
        cut.WaitForAssertion(() => cut.Find("[data-slot='autosave-status']").GetAttribute("data-state").Should().Be("saved"));
    }

    [Test]
    public async Task EditableAutoSave_is_opt_in_and_disabled_select_does_not_save()
    {
        var saves = 0;
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("a", "Alpha")])
            .Add(c => c.OnAutoSave, (_, _) => { saves++; return ValueTask.CompletedTask; }));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChanged.InvokeAsync("option:a"));
        saves.Should().Be(0);
        cut.FindAll("[data-slot='autosave-status']").Should().BeEmpty();
        cut.Render(p => p.Add(c => c.AutoSave, true).Add(c => c.Disabled, true).Add(c => c.Value, null));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChanged.InvokeAsync("option:a"));
        saves.Should().Be(0);
    }

    [Test]
    public async Task EditableAutoSave_input_group_saves_edits()
    {
        string? saved = null;
        var cut = Render<InputGroupInput>(p => p.Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (value, _) => { saved = value; return ValueTask.CompletedTask; }));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "updated" });
        cut.WaitForAssertion(() => saved.Should().Be("updated"));
    }

    [Test]
    public async Task EditableAutoSave_slider_waits_for_commit()
    {
        var saves = new List<double>();
        var cut = Render<Slider>(p => p.Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (value, _) => { saves.Add(value); return ValueTask.CompletedTask; }));
        await cut.InvokeAsync(() => cut.FindComponent<BradixSlider>().Instance.OnValuesChange.InvokeAsync(new double[] { 25 }));
        saves.Should().BeEmpty();
        await cut.InvokeAsync(() => cut.FindComponent<BradixSlider>().Instance.OnValueCommit.InvokeAsync(new double[] { 25 }));
        cut.WaitForAssertion(() => saves.Should().Equal(25));
    }

    [Test]
    public async Task EditableAutoSave_disposal_cancels_in_flight_save()
    {
        var started = new TaskCompletionSource<System.Threading.CancellationToken>(TaskCreationOptions.RunContinuationsAsynchronously);
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("a", "Alpha")])
            .Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, async (_, token) =>
            {
                started.TrySetResult(token);
                await Task.Delay(System.Threading.Timeout.Infinite, token);
            }));
        await cut.InvokeAsync(() => cut.FindComponent<Combobox>().Instance.ValueChanged.InvokeAsync("option:a"));
        var token = await started.Task.WaitAsync(TimeSpan.FromSeconds(5));
        await cut.InvokeAsync(() => cut.Instance.DisposeAsync().AsTask());
        token.IsCancellationRequested.Should().BeTrue();
    }

    [Test]
    public async Task EditableAutoSave_calendar_saves_selected_date()
    {
        DateOnly? saved = null;
        var cut = Render<Calendar>(p => p.Add(c => c.DisplayMonth, new DateOnly(2026, 9, 1))
            .Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (value, _) => { saved = value; return ValueTask.CompletedTask; }));
        await cut.Find("td[data-day='2026-09-15'] button").ClickAsync();
        cut.WaitForAssertion(() => saved.Should().Be(new DateOnly(2026, 9, 15)));
    }

    [Test]
    public async Task EditableAutoSave_rich_text_does_not_save_read_only_edits()
    {
        Services.AddScoped<IRichTextEditorInterop, RichTextEditorInterop>();
        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/richtextinterop.js");
        module.SetupVoid("mount", _ => true).SetVoidResult();
        module.SetupVoid("setContent", _ => true).SetVoidResult();
        module.SetupVoid("unmount", _ => true).SetVoidResult();
        string? saved = null;
        var cut = Render<RichTextEditor>(p => p.Add(c => c.ReadOnly, true)
            .Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (value, _) => { saved = value; return ValueTask.CompletedTask; }));
        await cut.InvokeAsync(() => cut.Instance.OnContentChanged("ignored"));
        saved.Should().BeNull();
        cut.Render(p => p.Add(c => c.ReadOnly, false));
        await cut.InvokeAsync(() => cut.Instance.OnContentChanged("<p>Edited</p>"));
        cut.WaitForAssertion(() => saved.Should().Be("<p>Edited</p>"));
    }

    [Test]
    public async Task EditableAutoSave_radio_saves_standalone_selection()
    {
        bool? saved = null;
        var cut = Render<Radio>(p => p.Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (value, _) => { saved = value; return ValueTask.CompletedTask; }));
        await cut.Find("input").ChangeAsync(new ChangeEventArgs { Value = true });
        cut.WaitForAssertion(() => saved.Should().BeTrue());
    }
}
