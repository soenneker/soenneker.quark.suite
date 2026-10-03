using System;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class AutoSaveControllerTests
{
    [Test]
    public void Disabled_autosave_has_no_per_input_allocation()
    {
        Func<Task> refresh = static () => Task.CompletedTask;
        for (int pass = 0; pass < 2; pass++)
        {
            long before = GC.GetAllocatedBytesForCurrentThread();
            for (int i = 0; i < 1000; i++)
            {
                var controller = new AutoSaveController<int>();
                controller.NotifyValueChanged(i, false, 750, null, default, refresh).GetAwaiter().GetResult();
                controller.Flush(i, false, null, default, refresh).GetAwaiter().GetResult();
                controller.DisposeAsync().GetAwaiter().GetResult();
            }
            long allocated = GC.GetAllocatedBytesForCurrentThread() - before;
            if (pass == 1) allocated.Should().Be(0);
        }
    }

    [Test]
    public async Task Enabling_saves_and_disabling_cancels_pending_value()
    {
        var controller = new AutoSaveController<int>();
        int saved = 0;
        Func<Task> refresh = static () => Task.CompletedTask;
        Func<int, CancellationToken, ValueTask> save = (value, _) => { saved = value; return ValueTask.CompletedTask; };
        await controller.SaveNow(7, true, save, default, refresh);
        saved.Should().Be(7);
        controller.State.Should().Be(AutoSaveState.Saved);
        controller.HasSaved.Should().BeTrue();
        await controller.NotifyValueChanged(8, true, 60000, save, default, refresh);
        controller.HasPendingValue.Should().BeTrue();
        await controller.NotifyValueChanged(9, false, 60000, save, default, refresh);
        await controller.Flush(9, false, save, default, refresh);
        saved.Should().Be(7);
        controller.State.Should().Be(AutoSaveState.Idle);
        controller.HasPendingValue.Should().BeFalse();
        controller.HasSaved.Should().BeFalse();
        await controller.DisposeAsync();
    }

    [Test]
    public async Task Disposing_before_first_use_prevents_later_save()
    {
        var controller = new AutoSaveController<int>();
        int saves = 0;
        Func<Task> refresh = static () => Task.CompletedTask;
        Func<int, CancellationToken, ValueTask> save = (_, _) => { saves++; return ValueTask.CompletedTask; };
        await controller.DisposeAsync();
        await controller.NotifyValueChanged(1, true, 0, save, default, refresh);
        await controller.SaveNow(2, true, save, default, refresh);
        await controller.DisposeAsync();
        saves.Should().Be(0);
        controller.HasPendingValue.Should().BeFalse();
    }
}
