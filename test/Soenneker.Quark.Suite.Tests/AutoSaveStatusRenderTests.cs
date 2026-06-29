using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AutoSaveStatus_saved_state_without_completed_save_does_not_render_status()
    {
        var cut = Render<AutoSaveStatus>(parameters => parameters
            .Add(p => p.State, AutoSaveState.Saved));

        cut.FindAll("[data-slot='autosave-status']").Should().BeEmpty();
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
