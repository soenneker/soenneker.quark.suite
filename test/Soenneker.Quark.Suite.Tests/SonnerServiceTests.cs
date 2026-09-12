using System;
using System.Threading.Tasks;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class SonnerServiceTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Pause_and_resume_preserve_active_or_initially_paused_timers(bool pauseBeforeCreation)
    {
        await using var service = new SonnerService { DefaultDuration = 100 };
        var closed = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        if (pauseBeforeCreation)
            await service.Pause(null, SonnerPosition.TopCenter);
        await service.Toast("Timed", options => options.OnAutoClose = () =>
        {
            closed.TrySetResult();
            return ValueTask.CompletedTask;
        });
        if (!pauseBeforeCreation)
            await service.Pause(null, SonnerPosition.TopCenter);
        await service.Pause(null, SonnerPosition.TopCenter);
        await Task.Delay(150);
        (await service.GetToasts())[0].Removed.Should().BeFalse();
        await service.Resume(null, SonnerPosition.TopCenter);
        await service.Resume(null, SonnerPosition.TopCenter);
        await closed.Task.WaitAsync(TimeSpan.FromSeconds(5));
        (await service.GetToasts()).Should().BeEmpty();
    }

    [Test]
    public async Task Dismissing_old_toast_cannot_remove_replacement_with_the_same_id()
    {
        await using var service = new SonnerService { DefaultDuration = 0 };
        var dismissed = 0;
        await service.Toast("Old", options =>
        {
            options.Id = "reused";
            options.OnDismiss = () => { dismissed++; return ValueTask.CompletedTask; };
        });
        var removal = service.Dismiss("reused");
        (await service.GetToasts())[0].Removed.Should().BeTrue();
        await service.Toast("Replacement", static options => options.Id = "reused");
        await removal;

        var remaining = await service.GetToasts();
        remaining.Count.Should().Be(1);
        remaining[0].Title.Should().Be("Replacement");
        remaining[0].Removed.Should().BeFalse();
        dismissed.Should().Be(1);
    }

    [Test]
    public async Task Snapshots_remain_independent_and_respect_mutable_creation_dates()
    {
        await using var service = new SonnerService { DefaultDuration = 0 };
        await service.Toast("First");
        var first = await service.GetToasts();
        await service.Toast("Second");
        var both = await service.GetToasts();
        first.Count.Should().Be(1);
        both.Count.Should().Be(2);
        first[0].CreatedAt = DateTimeOffset.MaxValue;
        var reordered = await service.GetToasts();
        reordered[0].Title.Should().Be("Second");
        both[0].Title.Should().Be("First");
    }

    [Test]
    public async Task Loading_defaults_can_be_overridden_without_changing_other_toast_defaults()
    {
        await using var service = new SonnerService { DefaultDuration = 0 };
        await service.Loading("Default");
        bool? observedDefault = null;
        await service.Loading("Overridden", options =>
        {
            observedDefault = options.Dismissible;
            options.Dismissible = true;
        });
        await service.Toast("Plain");
        var toasts = await service.GetToasts();
        observedDefault.Should().BeFalse();
        toasts[0].Dismissible.Should().BeFalse();
        toasts[1].Dismissible.Should().BeTrue();
        toasts[2].Dismissible.Should().BeTrue();
    }

    [Test]
    public void Public_toast_construction_still_generates_distinct_ids_and_timestamps()
    {
        var before = DateTimeOffset.UtcNow;
        var first = new SonnerToast();
        var second = new SonnerToast();
        first.Id.Should().NotBeNullOrEmpty().And.NotBe(second.Id);
        first.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(DateTimeOffset.UtcNow);
        first.Mounted.Should().BeTrue();
    }

    #if !DEBUG
    // Debug async state machines allocate independently of the production hot path.
    [Test]
    public async Task Empty_snapshots_and_idle_pause_resume_do_not_allocate_per_call()
    {
        await using var service = new SonnerService();
        for (var i = 0; i < 100; i++)
        {
            _ = await service.GetToasts();
            await service.Pause(null, SonnerPosition.TopCenter);
            await service.Resume(null, SonnerPosition.TopCenter);
        }

        // Uncontended ValueTasks complete synchronously, so the loop stays on this thread.
        long before = GC.GetAllocatedBytesForCurrentThread();
        for (var i = 0; i < 1000; i++)
        {
            var snapshot = service.GetToasts();
            if (!snapshot.IsCompletedSuccessfully) throw new InvalidOperationException("Unexpected contention");
            _ = snapshot.Result;
            service.Pause(null, SonnerPosition.TopCenter).GetAwaiter().GetResult();
            service.Resume(null, SonnerPosition.TopCenter).GetAwaiter().GetResult();
        }
        long allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        allocated.Should().Be(0);
    }
    #endif
}
