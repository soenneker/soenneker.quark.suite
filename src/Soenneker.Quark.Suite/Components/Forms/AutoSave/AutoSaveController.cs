using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal sealed class AutoSaveController<TValue> : IAsyncDisposable
{
    private const int MinimumSavingStateDuration = 500;

    private CancellationTokenSource? _operationCancellationTokenSource;
    private int _version;
    private bool _hasPendingValue;
    private TValue _pendingValue = default!;
    private bool _disposed;

    public AutoSaveState State { get; private set; } = AutoSaveState.Idle;

    public bool HasPendingValue => _hasPendingValue;

    public bool HasSaved { get; private set; }

    public Task NotifyValueChanged(TValue value, bool autoSave, int autoSaveDelay, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed)
            return Task.CompletedTask;

        if (!CanAutoSave(autoSave, onAutoSave))
        {
            CancelOperation();
            _hasPendingValue = false;
            HasSaved = false;
            QueueSetState(AutoSaveState.Idle, autoSaveStateChanged, refreshAsync);
            return Task.CompletedTask;
        }

        _pendingValue = value;
        _hasPendingValue = true;

        CancelOperation();
        var version = NextVersion();
        var delay = Math.Max(0, autoSaveDelay);
        CancellationToken cancellationToken = CreateOperationCancellationToken();

        QueueSetState(AutoSaveState.Pending, autoSaveStateChanged, refreshAsync);

        _ = RunDelayedSave(value, delay, version, onAutoSave!, autoSaveStateChanged, refreshAsync, cancellationToken);
        return Task.CompletedTask;
    }

    public Task Flush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed || !_hasPendingValue)
            return Task.CompletedTask;

        var value = _pendingValue;
        return SaveNow(value, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync);
    }

    public void QueueFlush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed || !_hasPendingValue || !CanAutoSave(autoSave, onAutoSave))
            return;

        _ = RunQueuedFlush(currentValue, autoSave, onAutoSave, autoSaveStateChanged, refreshAsync);
    }

    public Task SaveNow(TValue value, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        if (_disposed || !CanAutoSave(autoSave, onAutoSave))
            return Task.CompletedTask;

        CancelOperation();
        var version = NextVersion();
        CancellationToken cancellationToken = CreateOperationCancellationToken();

        return RunSave(value, version, onAutoSave!, autoSaveStateChanged, refreshAsync, cancellationToken);
    }

    private async Task RunQueuedFlush(TValue currentValue, bool autoSave, Func<TValue, CancellationToken, ValueTask>? onAutoSave,
        EventCallback<AutoSaveState> autoSaveStateChanged, Func<Task> refreshAsync)
    {
        await Task.Yield();

        if (_disposed)
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
        long savingStarted = Environment.TickCount64;

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
        long elapsed = Environment.TickCount64 - savingStarted;
        long remaining = MinimumSavingStateDuration - elapsed;

        if (remaining > 0)
            await Task.Delay((int)remaining, cancellationToken);
    }

    private int NextVersion()
    {
        unchecked
        {
            return ++_version;
        }
    }

    private bool IsCurrent(int version) => version == _version;

    private CancellationToken CreateOperationCancellationToken()
    {
        _operationCancellationTokenSource = new CancellationTokenSource();
        return _operationCancellationTokenSource.Token;
    }

    private void CancelOperation()
    {
        _operationCancellationTokenSource?.Cancel();
        _operationCancellationTokenSource?.Dispose();
        _operationCancellationTokenSource = null;
    }

    public ValueTask DisposeAsync()
    {
        _disposed = true;
        CancelOperation();
        return ValueTask.CompletedTask;
    }
}
