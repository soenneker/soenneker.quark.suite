using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

// Kept as a mutable component field. The ordinary, non-autosaving input stores only
// this reference; operation state and synchronization are allocated on first use.
internal struct AutoSaveController<TValue> : IAsyncDisposable
{
    private static readonly AutoSaveOperationController<TValue> Disposed = new(disposed: true);
    private AutoSaveOperationController<TValue>? _controller;

    public readonly AutoSaveState State => _controller?.State ?? AutoSaveState.Idle;
    public readonly bool HasPendingValue => _controller?.HasPendingValue ?? false;
    public readonly bool HasSaved => _controller?.HasSaved ?? false;

    private AutoSaveOperationController<TValue> GetOrCreate()
    {
        var existing = Volatile.Read(ref _controller);
        if (existing is not null) return existing;
        var created = new AutoSaveOperationController<TValue>();
        return Interlocked.CompareExchange(ref _controller, created, null) ?? created;
    }

    public Task NotifyValueChanged(TValue value, bool autoSave, int autoSaveDelay,
        Func<TValue, CancellationToken, ValueTask>? onAutoSave, EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_controller is null && (!autoSave || onAutoSave is null)) return Task.CompletedTask;
        return GetOrCreate().NotifyValueChanged(value, autoSave, autoSaveDelay, onAutoSave, autoSaveStateChanged, refreshAsync);
    }

    public readonly Task Flush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync) =>
        _controller?.Flush(currentValue, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync) ?? Task.CompletedTask;

    public readonly void QueueFlush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync) =>
        _controller?.QueueFlush(currentValue, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync);

    public Task SaveNow(TValue value, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (!autoSave || onAutoSave is null) return Task.CompletedTask;
        return GetOrCreate().SaveNow(value, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync);
    }

    public ValueTask DisposeAsync()
    {
        var previous = Interlocked.Exchange(ref _controller, Disposed);
        return previous is null || ReferenceEquals(previous, Disposed) ? ValueTask.CompletedTask : previous.DisposeAsync();
    }
}
