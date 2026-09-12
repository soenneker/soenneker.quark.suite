using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class QuarkDateTimeSchedulerTests
{
    [Test]
    public async Task Rescheduling_an_inactive_registration_starts_the_runner()
    {
        await using var scheduler = Create();
        var tick = Signal();
        bool enabled = false;
        using var registration = scheduler.Register(_ => enabled ? TimeSpan.FromMilliseconds(1) : null, _ =>
        {
            enabled = false;
            tick.TrySetResult();
            return ValueTask.CompletedTask;
        });
        enabled = true;
        registration.Reschedule();
        await tick.Task.WaitAsync(TimeSpan.FromSeconds(5));
    }

    [Test]
    public async Task Registration_bursts_wake_once_without_allocating_a_source_per_reschedule()
    {
        await using var scheduler = Create();
        var entered = Signal();
        var release = Signal();
        using var active = scheduler.Register(static _ => TimeSpan.FromMilliseconds(1), async _ =>
        {
            entered.TrySetResult();
            await release.Task;
        });
        await entered.Task.WaitAsync(TimeSpan.FromSeconds(5));
        try
        {
            using var waiting = scheduler.Register(static _ => TimeSpan.FromHours(1), static _ => ValueTask.CompletedTask);
            waiting.Reschedule();
            long before = GC.GetAllocatedBytesForCurrentThread();
            for (var i = 0; i < 1000; i++)
                waiting.Reschedule();
            long allocated = GC.GetAllocatedBytesForCurrentThread() - before;
            await Assert.That(allocated).IsLessThan(1024);
        }
        finally
        {
            active.Dispose();
            release.TrySetResult();
        }
    }

    [Test]
    public async Task Callbacks_can_reschedule_and_unregister_without_overlapping()
    {
        await using var scheduler = Create();
        var completed = Signal();
        var registered = Signal();
        var callbackCount = 0;
        var inCallback = 0;
        var overlapped = false;
        IQuarkDateTimeScheduleRegistration? registration = null;
        registration = scheduler.Register(static _ => TimeSpan.FromMilliseconds(10), async _ =>
        {
            await registered.Task;
            if (Interlocked.Increment(ref inCallback) != 1)
                overlapped = true;
            registration!.Reschedule();
            await Task.Yield();
            if (++callbackCount == 3)
            {
                registration.Dispose();
                completed.TrySetResult();
            }
            Interlocked.Decrement(ref inCallback);
        });
        registered.TrySetResult();
        using (registration)
            await completed.Task.WaitAsync(TimeSpan.FromSeconds(5));
        await Assert.That(overlapped).IsFalse();
        await Assert.That(callbackCount).IsEqualTo(3);
    }

    [Test]
    public async Task Disposed_registration_is_skipped_while_another_callback_is_awaiting()
    {
        await using var scheduler = Create();
        var entered = Signal();
        var release = Signal();
        var finished = Signal();
        var unwantedCalls = 0;
        using var first = scheduler.Register(static _ => TimeSpan.FromMilliseconds(1), async _ =>
        {
            entered.TrySetResult();
            await release.Task;
        });
        await entered.Task.WaitAsync(TimeSpan.FromSeconds(5));
        try
        {
            var second = scheduler.Register(static _ => TimeSpan.FromMilliseconds(1), _ =>
            {
                Interlocked.Increment(ref unwantedCalls);
                return ValueTask.CompletedTask;
            });
            second.Dispose();
            using var sentinel = scheduler.Register(static _ => TimeSpan.FromMilliseconds(1), _ =>
            {
                finished.TrySetResult();
                return ValueTask.CompletedTask;
            });
            first.Dispose();
            release.TrySetResult();
            await finished.Task.WaitAsync(TimeSpan.FromSeconds(5));
            await Assert.That(unwantedCalls).IsEqualTo(0);
        }
        finally
        {
            first.Dispose();
            release.TrySetResult();
        }
    }

    [Test]
    public async Task Disposal_interrupts_a_long_delay()
    {
        var scheduler = Create();
        using var registration = scheduler.Register(static _ => TimeSpan.FromHours(1), static _ => ValueTask.CompletedTask);
        await scheduler.DisposeAsync().AsTask().WaitAsync(TimeSpan.FromSeconds(5));
        registration.Reschedule();
        await scheduler.DisposeAsync();
    }

    private static QuarkDateTimeScheduler Create() => new(NullLogger<QuarkDateTimeScheduler>.Instance);
    private static TaskCompletionSource Signal() => new(TaskCreationOptions.RunContinuationsAsynchronously);
}
