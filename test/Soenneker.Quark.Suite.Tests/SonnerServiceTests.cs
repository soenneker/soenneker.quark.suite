using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed class SonnerServiceTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Promise_preserves_configuration_precedence_for_success_and_error(bool fail, CancellationToken cancellationToken)
    {
        await using var service = new SonnerService { DefaultDuration = 0 };
        var gate = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        int successCalls = 0, errorCalls = 0;
        string id = await service.Promise(new ValueTask(gate.Task), new SonnerPromiseOptions
        {
            Loading = "Loading", Success = "Saved", Error = "Failed", Description = "fallback",
            SuccessDescription = "success description", ErrorDescription = "error description",
            ConfigureLoading = options => { options.Id = "promise"; options.Description = "loading override"; },
            ConfigureSuccess = options => { successCalls++; options.Description = "success override"; },
            ConfigureError = options => { errorCalls++; options.Description = "error override"; }
        }, cancellationToken);
        id.Should().Be("promise");
        (await service.GetToasts(cancellationToken: cancellationToken))[0].Description.Should().Be("loading override");
        if (fail) gate.SetException(new InvalidOperationException("failure")); else gate.SetResult();
        var timeout = DateTime.UtcNow.AddSeconds(5);
        while ((await service.GetToasts(cancellationToken: cancellationToken))[0].Type == SonnerToastType.Loading && DateTime.UtcNow < timeout)
            await Task.Delay(10, cancellationToken: cancellationToken);
        var toast = (await service.GetToasts(cancellationToken: cancellationToken)).Should().ContainSingle().Subject;
        toast.Id.Should().Be(id);
        toast.Title.Should().Be(fail ? "Failed" : "Saved");
        toast.Description.Should().Be(fail ? "error override" : "success override");
        successCalls.Should().Be(fail ? 0 : 1);
        errorCalls.Should().Be(fail ? 1 : 0);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async ValueTask Pause_and_resume_preserve_active_or_initially_paused_timers(bool pauseBeforeCreation, CancellationToken cancellationToken)
    {
        await using var service = new SonnerService { DefaultDuration = 100 };
        var closed = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        if (pauseBeforeCreation)
            await service.Pause(null, SonnerPosition.TopCenter, cancellationToken: cancellationToken);
        await service.Toast("Timed", options => options.OnAutoClose = () =>
        {
            closed.TrySetResult();
            return ValueTask.CompletedTask;
        }, cancellationToken: cancellationToken);
        if (!pauseBeforeCreation)
            await service.Pause(null, SonnerPosition.TopCenter, cancellationToken: cancellationToken);
        await service.Pause(null, SonnerPosition.TopCenter, cancellationToken: cancellationToken);
        await Task.Delay(150, cancellationToken: cancellationToken);
        (await service.GetToasts(cancellationToken: cancellationToken))[0].Removed.Should().BeFalse();
        await service.Resume(null, SonnerPosition.TopCenter, cancellationToken: cancellationToken);
        await service.Resume(null, SonnerPosition.TopCenter, cancellationToken: cancellationToken);
        await closed.Task.WaitAsync(TimeSpan.FromSeconds(5), cancellationToken: cancellationToken);
        (await service.GetToasts(cancellationToken: cancellationToken)).Should().BeEmpty();
    }

    [Test]
    public async ValueTask Dismissing_old_toast_cannot_remove_replacement_with_the_same_id(CancellationToken cancellationToken)
    {
        await using var service = new SonnerService { DefaultDuration = 0 };
        var dismissed = 0;
        await service.Toast("Old", options =>
        {
            options.Id = "reused";
            options.OnDismiss = () => { dismissed++; return ValueTask.CompletedTask; };
        }, cancellationToken: cancellationToken);
        var removal = service.Dismiss("reused", cancellationToken: cancellationToken);
        (await service.GetToasts(cancellationToken: cancellationToken))[0].Removed.Should().BeTrue();
        await service.Toast("Replacement", static options => options.Id = "reused", cancellationToken: cancellationToken);
        await removal;

        var remaining = await service.GetToasts(cancellationToken: cancellationToken);
        remaining.Count.Should().Be(1);
        remaining[0].Title.Should().Be("Replacement");
        remaining[0].Removed.Should().BeFalse();
        dismissed.Should().Be(1);
    }

    [Test]
    public async ValueTask Snapshots_remain_independent_and_respect_mutable_creation_dates(CancellationToken cancellationToken)
    {
        await using var service = new SonnerService { DefaultDuration = 0 };
        await service.Toast("First", cancellationToken: cancellationToken);
        var first = await service.GetToasts(cancellationToken: cancellationToken);
        await service.Toast("Second", cancellationToken: cancellationToken);
        var both = await service.GetToasts(cancellationToken: cancellationToken);
        first.Count.Should().Be(1);
        both.Count.Should().Be(2);
        first[0].CreatedAt = DateTimeOffset.MaxValue;
        var reordered = await service.GetToasts(cancellationToken: cancellationToken);
        reordered[0].Title.Should().Be("Second");
        both[0].Title.Should().Be("First");
    }

    [Test]
    public async ValueTask Loading_defaults_can_be_overridden_without_changing_other_toast_defaults(CancellationToken cancellationToken)
    {
        await using var service = new SonnerService { DefaultDuration = 0 };
        await service.Loading("Default", cancellationToken: cancellationToken);
        bool? observedDefault = null;
        await service.Loading("Overridden", options =>
        {
            observedDefault = options.Dismissible;
            options.Dismissible = true;
        }, cancellationToken: cancellationToken);
        await service.Toast("Plain", cancellationToken: cancellationToken);
        var toasts = await service.GetToasts(cancellationToken: cancellationToken);
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
    public async ValueTask Empty_snapshots_and_idle_pause_resume_do_not_allocate_per_call(CancellationToken cancellationToken)
    {
        await using var service = new SonnerService();
        // Keep the default-token path here to isolate the service's allocation baseline.
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
