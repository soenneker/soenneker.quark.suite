using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AutoSaveStatus_saved_state_without_completed_save_reserves_silent_status_slot()
    {
        var cut = Render<AutoSaveStatus>(parameters => parameters
            .Add(p => p.State, AutoSaveState.Saved));

        var status = cut.Find("[data-slot='autosave-status']");

        status.ClassList.Should().Contain("size-4");
        status.GetAttribute("aria-hidden").Should().Be("true");
        status.HasAttribute("role").Should().BeFalse();
        status.HasAttribute("aria-live").Should().BeFalse();
    }

    [Test]
    public void AutoSaveStatus_saved_state_after_completed_save_renders_icon_only_status()
    {
        var cut = Render<AutoSaveStatus>(parameters => parameters
            .Add(p => p.State, AutoSaveState.Saved)
            .Add(p => p.HasSaved, true));

        var status = cut.Find("[data-slot='autosave-status']");

        status.GetAttribute("data-state").Should().Be("saved");
        status.TextContent.Should().BeNullOrWhiteSpace();
    }

    [Test]
    public void AutoSaveStatus_pending_state_reserves_silent_status_slot()
    {
        var cut = Render<AutoSaveStatus>(parameters => parameters
            .Add(p => p.State, AutoSaveState.Pending));

        var status = cut.Find("[data-slot='autosave-status']");

        status.ClassList.Should().Contain("size-4");
        status.GetAttribute("aria-hidden").Should().Be("true");
        status.HasAttribute("role").Should().BeFalse();
        status.HasAttribute("aria-live").Should().BeFalse();
    }

    [Test]
    public async Task AutoSave_debounces_initial_value_change_before_saving()
    {
        var saveCount = 0;
        string? savedValue = null;
        var states = new List<AutoSaveState>();
        var gate = new object();

        Func<string?, CancellationToken, ValueTask> onAutoSave = (value, _) =>
        {
            saveCount++;
            savedValue = value;
            return ValueTask.CompletedTask;
        };

        var cut = Render<TextInput>(parameters => parameters
            .Add(p => p.Value, string.Empty)
            .Add(p => p.AutoSave, true)
            .Add(p => p.AutoSaveDelay, 250)
            .Add(p => p.OnAutoSave, onAutoSave)
            .Add(p => p.AutoSaveStateChanged, state =>
            {
                lock (gate)
                {
                    states.Add(state);
                }

                return Task.CompletedTask;
            }));

        cut.Find("input").Input("f");

        await Task.Delay(100);

        saveCount.Should().Be(0);
        HasState(AutoSaveState.Saving).Should().BeFalse();

        cut.Find("input").Input("fi");

        await Task.Delay(100);

        saveCount.Should().Be(0);
        HasState(AutoSaveState.Saving).Should().BeFalse();

        cut.WaitForAssertion(() =>
        {
            saveCount.Should().Be(1);
            savedValue.Should().Be("fi");
        }, TimeSpan.FromSeconds(2));

        bool HasState(AutoSaveState state)
        {
            lock (gate)
            {
                return states.Any(entry => EqualityComparer<AutoSaveState>.Default.Equals(entry, state));
            }
        }
    }

    [Test]
    public async Task AutoSave_input_does_not_wait_for_pending_state_notification()
    {
        var pendingStateGate = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        Func<string?, CancellationToken, ValueTask> onAutoSave = (_, _) => ValueTask.CompletedTask;

        var cut = Render<TextInput>(parameters => parameters
            .Add(p => p.Value, string.Empty)
            .Add(p => p.AutoSave, true)
            .Add(p => p.AutoSaveDelay, 1_000)
            .Add(p => p.OnAutoSave, onAutoSave)
            .Add(p => p.AutoSaveStateChanged, state => state == AutoSaveState.Pending
                ? pendingStateGate.Task
                : Task.CompletedTask));

        Task inputTask = cut.Find("input").InputAsync(new ChangeEventArgs { Value = "f" });
        Task completed = await Task.WhenAny(inputTask, Task.Delay(250));

        completed.Should().Be(inputTask);
        pendingStateGate.SetResult();
    }

    [Test]
    public async Task AutoSave_keeps_saving_state_visible_for_minimum_duration()
    {
        var stopwatch = new Stopwatch();
        var states = new List<(AutoSaveState State, long Milliseconds)>();
        var gate = new object();
        Func<string?, CancellationToken, ValueTask> onAutoSave = (_, _) => ValueTask.CompletedTask;

        var cut = Render<TextInput>(parameters => parameters
            .Add(p => p.Value, string.Empty)
            .Add(p => p.AutoSave, true)
            .Add(p => p.AutoSaveDelay, 0)
            .Add(p => p.OnAutoSave, onAutoSave)
            .Add(p => p.AutoSaveStateChanged, state =>
            {
                lock (gate)
                {
                    states.Add((state, stopwatch.ElapsedMilliseconds));
                }

                return Task.CompletedTask;
            }));

        stopwatch.Start();
        cut.Find("input").Input("server work");

        cut.WaitForAssertion(() => HasState(AutoSaveState.Saving).Should().BeTrue());

        await Task.Delay(100);

        HasState(AutoSaveState.Saved).Should().BeFalse();

        cut.WaitForAssertion(() => HasState(AutoSaveState.Saved).Should().BeTrue(), TimeSpan.FromSeconds(2));

        ElapsedBetween(AutoSaveState.Saving, AutoSaveState.Saved).Should().BeGreaterThanOrEqualTo(450);

        bool HasState(AutoSaveState state)
        {
            lock (gate)
            {
                return states.Any(entry => EqualityComparer<AutoSaveState>.Default.Equals(entry.State, state));
            }
        }

        long ElapsedBetween(AutoSaveState start, AutoSaveState end)
        {
            lock (gate)
            {
                long startMilliseconds = states.First(entry => EqualityComparer<AutoSaveState>.Default.Equals(entry.State, start)).Milliseconds;
                long endMilliseconds = states.First(entry => EqualityComparer<AutoSaveState>.Default.Equals(entry.State, end)).Milliseconds;

                return endMilliseconds - startMilliseconds;
            }
        }
    }
}
