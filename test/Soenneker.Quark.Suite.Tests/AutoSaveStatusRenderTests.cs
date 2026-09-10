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
        const int debounceDelay = 250;
        var stopwatch = new Stopwatch();
        var saves = new List<(string? Value, long Milliseconds)>();
        var savingTimes = new List<long>();
        var gate = new object();

        Func<string?, CancellationToken, ValueTask> onAutoSave = (value, _) =>
        {
            lock (gate)
            {
                saves.Add((value, stopwatch.ElapsedMilliseconds));
            }

            return ValueTask.CompletedTask;
        };

        var cut = Render<TextInput>(parameters => parameters
            .Add(p => p.Value, string.Empty)
            .Add(p => p.AutoSave, true)
            .Add(p => p.AutoSaveDelay, debounceDelay)
            .Add(p => p.OnAutoSave, onAutoSave)
            .Add(p => p.AutoSaveStateChanged, state =>
            {
                if (state == AutoSaveState.Saving)
                {
                    lock (gate)
                    {
                        savingTimes.Add(stopwatch.ElapsedMilliseconds);
                    }
                }

                return Task.CompletedTask;
            }));

        stopwatch.Start();
        cut.Find("input").Input("f");

        // A busy runner can resume this wait after the debounce has already expired.
        // Check the actual save timestamps instead of assuming the wait resumes on time.
        await Task.Delay(100);
        var secondInputTime = stopwatch.ElapsedMilliseconds;
        cut.Find("input").Input("fi");

        cut.WaitForAssertion(() =>
        {
            lock (gate)
            {
                saves.Should().ContainSingle(save => save.Value == "fi");
                saves.Should().OnlyContain(save => save.Value == "f" || save.Value == "fi");
                saves.Count(save => save.Value == "f").Should().BeLessThanOrEqualTo(1);
                foreach (var save in saves)
                {
                    var inputTime = save.Value == "fi" ? secondInputTime : 0;
                    (save.Milliseconds - inputTime).Should().BeGreaterThanOrEqualTo(debounceDelay);
                }

                savingTimes.Should().NotBeEmpty();
                savingTimes.Should().OnlyContain(time => time >= debounceDelay);
            }
        }, TimeSpan.FromSeconds(2));
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

        var inputTask = cut.Find("input").InputAsync(new ChangeEventArgs { Value = "f" });
        var completed = await Task.WhenAny(inputTask, Task.Delay(250));

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
                var startMilliseconds = states.First(entry => EqualityComparer<AutoSaveState>.Default.Equals(entry.State, start)).Milliseconds;
                var endMilliseconds = states.First(entry => EqualityComparer<AutoSaveState>.Default.Equals(entry.State, end)).Milliseconds;

                return endMilliseconds - startMilliseconds;
            }
        }
    }
}
