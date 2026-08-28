using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Asyncs.Locks;
using Soenneker.Atomics.ValueBools;

namespace Soenneker.Quark;

internal sealed class AutoSaveController<TValue> : IAsyncDisposable
{
    private const int MinimumSavingStateDuration = 500;

    private readonly AsyncLock _operationLock = new();
    private CancellationTokenSource? _operationCancellationTokenSource;
    private int _version;
    private bool _hasPendingValue;
    private TValue _pendingValue = default!;
    private ValueAtomicBool _disposed = new(false);

    public AutoSaveState State { get; private set; } = AutoSaveState.Idle;

    public bool HasPendingValue => _hasPendingValue;

    public bool HasSaved { get; private set; }

    public async Task NotifyValueChanged(TValue value, bool autoSave, int autoSaveDelay, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed.Value)
            return;

        if (!CanAutoSave(autoSave, onAutoSave))
        {
            await CancelOperation();
            _hasPendingValue = false;
            HasSaved = false;
            QueueSetState(AutoSaveState.Idle, autoSaveStateChanged, refreshAsync);
            return;
        }

        _pendingValue = value;
        _hasPendingValue = true;

        var delay = Math.Max(0, autoSaveDelay);

        var operation = await TryStartOperation();
        if (operation is null)
            return;

        QueueSetState(AutoSaveState.Pending, autoSaveStateChanged, refreshAsync);

        _ = RunDelayedSave(value, delay, operation.Value.Version, onAutoSave!, autoSaveStateChanged, refreshAsync, operation.Value.CancellationToken);
    }

    public Task Flush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed.Value || !_hasPendingValue)
            return Task.CompletedTask;

        var value = _pendingValue;
        return SaveNow(value, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync);
    }

    public void QueueFlush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed.Value || !_hasPendingValue || !CanAutoSave(autoSave, onAutoSave))
            return;

        _ = RunQueuedFlush(currentValue, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync);
    }

    public async Task SaveNow(TValue value, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed.Value || !CanAutoSave(autoSave, onAutoSave))
            return;

        var operation = await TryStartOperation();
        if (operation is null)
            return;

        await RunSave(value, operation.Value.Version, onAutoSave!, autoSaveStateChanged, refreshAsync, operation.Value.CancellationToken);
    }

    private async Task RunQueuedFlush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        await Task.Yield();

        if (_disposed.Value)
            return;

        await Flush(currentValue, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync);
    }

    private async Task RunDelayedSave(TValue value, int delay, int version, Func<TValue, CancellationToken, ValueTask> onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync, CancellationToken cancellationToken)
    {
        try
        {
            if (delay > 0)
                await Task.Delay(delay, cancellationToken);

            await RunSave(value, version, onAutoSave, autoSaveStateChanged, refreshAsync, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    private async Task RunSave(TValue value, int version, Func<TValue, CancellationToken, ValueTask> onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync, CancellationToken cancellationToken)
    {
        if (!IsCurrent(version) || cancellationToken.IsCancellationRequested)
            return;

        _hasPendingValue = false;
        await SetState(AutoSaveState.Saving, autoSaveStateChanged, refreshAsync);
        var savingStarted = Environment.TickCount64;

        try
        {
            await onAutoSave(value, cancellationToken);
            await DelayForMinimumSavingStateDuration(savingStarted, cancellationToken);

            if (IsCurrent(version) && !cancellationToken.IsCancellationRequested)
            {
                HasSaved = true;
                await SetState(AutoSaveState.Saved, autoSaveStateChanged, refreshAsync);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        catch
        {
            try
            {
                await DelayForMinimumSavingStateDuration(savingStarted, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (IsCurrent(version) && !cancellationToken.IsCancellationRequested)
                await SetState(AutoSaveState.Failed, autoSaveStateChanged, refreshAsync);
        }
    }

    private async Task SetState(AutoSaveState state, EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (EqualityComparer<AutoSaveState>.Default.Equals(State, state))
            return;

        State = state;
        await NotifyStateChanged(state, autoSaveStateChanged, refreshAsync);
    }

    private void QueueSetState(AutoSaveState state, EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (EqualityComparer<AutoSaveState>.Default.Equals(State, state))
            return;

        State = state;
        _ = NotifyStateChangedSafely(state, autoSaveStateChanged, refreshAsync);
    }

    private static async Task NotifyStateChanged(AutoSaveState state, EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (autoSaveStateChanged.HasDelegate)
            await autoSaveStateChanged.InvokeAsync(state);

        await refreshAsync();
    }

    private static async Task NotifyStateChangedSafely(AutoSaveState state, EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        try
        {
            await NotifyStateChanged(state, autoSaveStateChanged, refreshAsync);
        }
        catch
        {
        }
    }

    private static bool CanAutoSave(bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave)
    {
        return autoSave && onAutoSave is not null;
    }

    private static async Task DelayForMinimumSavingStateDuration(long savingStarted, CancellationToken cancellationToken)
    {
        var elapsed = Environment.TickCount64 - savingStarted;
        var remaining = MinimumSavingStateDuration - elapsed;

        if (remaining > 0)
            await Task.Delay((int)remaining, cancellationToken);
    }

    private async ValueTask<Operation?> TryStartOperation()
    {
        CancellationTokenSource? previous;
        Operation operation;

        using (await _operationLock.Lock())
        {
            if (_disposed.Value)
                return null;

            previous = _operationCancellationTokenSource;
            _operationCancellationTokenSource = new CancellationTokenSource();

            unchecked
            {
                _version++;
            }

            operation = new Operation(_version, _operationCancellationTokenSource.Token);
        }

        CancelAndDispose(previous);
        return operation;
    }

    private bool IsCurrent(int version) => version == Volatile.Read(ref _version);

    private async ValueTask CancelOperation()
    {
        CancellationTokenSource? source;

        using (await _operationLock.Lock())
        {
            source = _operationCancellationTokenSource;
            _operationCancellationTokenSource = null;

            unchecked
            {
                _version++;
            }
        }

        CancelAndDispose(source);
    }

    private static void CancelAndDispose(CancellationTokenSource? source)
    {
        if (source is null)
            return;

        source.Cancel();
        source.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed.TrySetTrue())
            return;

        await CancelOperation();
    }

    private readonly record struct Operation(int Version, CancellationToken CancellationToken);
}
