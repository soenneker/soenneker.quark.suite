using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Asyncs.Locks;
using Soenneker.Atomics.ValueBools;
using Soenneker.Blazor.Utils.Ids;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISonnerService"/>
public sealed class SonnerService : ISonnerService
{
    private const int _mountDelayMs = 16;
    private const int _removeDelayMs = 400;
    private const string _defaultToasterId = "";
    private readonly AsyncLock _sync = new();
    private readonly List<SonnerToast> _toasts = [];
    private readonly Dictionary<string, SonnerTimerState> _timers = new(StringComparer.Ordinal);
    private readonly Dictionary<string, SonnerToasterRegistration> _toasters = new(StringComparer.Ordinal);
    private readonly HashSet<(string ToasterId, SonnerPosition Position)> _pausedToasters = [];
    private string _activeToasterId = _defaultToasterId;
    private ValueAtomicBool _disposed = new(false);

    public event Action? StateChanged;

    public SonnerPosition DefaultPosition { get; set; } = SonnerPosition.TopCenter;

    public int DefaultDuration { get; set; } = 4000;

    public bool DefaultCloseButton { get; set; }

    public async ValueTask<IReadOnlyList<SonnerToast>> GetToasts(CancellationToken cancellationToken = default)
    {
        using (await _sync.Lock(cancellationToken))
        {
            if (_toasts.Count == 0)
                return Array.Empty<SonnerToast>();

            var toasts = _toasts.ToArray();

            Array.Sort(toasts, static (left, right) => left.CreatedAt.CompareTo(right.CreatedAt));
            return toasts;
        }
    }

    public ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Default, configure, cancellationToken);
    }

    public ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(null, content, SonnerToastType.Default, configure, cancellationToken);
    }

    public ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Success, configure, cancellationToken);
    }

    public ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Info, configure, cancellationToken);
    }

    public ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Warning, configure, cancellationToken);
    }

    public ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Error, configure, cancellationToken);
    }

    public ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Loading, configure, cancellationToken);
    }

    public ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(null, content, SonnerToastType.Default, configure, cancellationToken);
    }

    public async ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options, CancellationToken cancellationToken)
    {
        var id = await CreateOrUpdate(options.Loading, null, SonnerToastType.Loading, options.ConfigureLoading,
            cancellationToken, description: options.Description);

        using (await _sync.Lock(cancellationToken))
        {
            var existing = FindToast(id);
            if (existing is not null)
                existing.Promise = true;
        }

        _ = HandlePromise(task, id, options, cancellationToken);
        return id;
    }

    public ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Promise(taskFactory(), options, cancellationToken);
    }

    public async ValueTask RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton,
        CancellationToken cancellationToken = default)
    {
        var normalizedToasterId = NormalizeToasterId(toasterId);

        using (await _sync.Lock(cancellationToken))
        {
            if (!_toasters.TryGetValue(normalizedToasterId, out var existing) || !Equals(existing.DefaultPosition, defaultPosition) ||
                existing.DefaultDuration != defaultDuration || existing.CloseButton != closeButton)
                _toasters[normalizedToasterId] = new SonnerToasterRegistration(defaultPosition, defaultDuration, closeButton);
            _activeToasterId = normalizedToasterId;
        }
    }

    public async ValueTask UnregisterToaster(string? toasterId, CancellationToken cancellationToken = default)
    {
        var normalizedToasterId = NormalizeToasterId(toasterId);

        using (await _sync.Lock(cancellationToken))
        {
            _toasters.Remove(normalizedToasterId);
            foreach (var item in _pausedToasters)
                if (item.ToasterId == normalizedToasterId)
                    _pausedToasters.Remove(item);

            if (_activeToasterId == normalizedToasterId)
            {
                if (_toasters.ContainsKey(_defaultToasterId))
                    _activeToasterId = _defaultToasterId;
                else
                    _activeToasterId = FirstToasterIdOrDefault();
            }
        }
    }

    public async ValueTask Pause(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default)
    {
        if (_disposed.Value)
            return;

        SmallBatch<CancellationTokenSource> timersToCancel = default;
        var now = DateTimeOffset.UtcNow;
        var normalizedToasterId = NormalizeToasterId(toasterId);
        var pausedKey = (normalizedToasterId, position);

        using (await _sync.Lock(cancellationToken))
        {
            if (!_pausedToasters.Add(pausedKey))
                return;

            foreach (var toast in _toasts)
            {
                if (toast.ToasterId != normalizedToasterId || toast.Position != position || toast.Removed)
                    continue;

                if (!_timers.TryGetValue(toast.Id, out var timerState))
                    continue;

                if (timerState.Paused || timerState.CancellationTokenSource is null)
                    continue;

                timerState.RemainingMs = GetRemainingMs(timerState, now);
                timerState.Paused = true;
                timersToCancel.Add(timerState.CancellationTokenSource);
                timerState.CancellationTokenSource = null;
            }
        }

        if (timersToCancel.Count == 0)
            return;

        for (var index = 0; index < timersToCancel.Count; index++)
        {
            var timer = timersToCancel[index];
            await timer.CancelAsync();
            timer.Dispose();
        }
    }

    public async ValueTask Resume(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default)
    {
        if (_disposed.Value)
            return;

        SmallBatch<(SonnerToast Toast, int RemainingMs, CancellationToken Token)> timersToStart = default;
        SmallBatch<SonnerToast> toastsToDismiss = default;
        var normalizedToasterId = NormalizeToasterId(toasterId);
        var pausedKey = (normalizedToasterId, position);

        using (await _sync.Lock(cancellationToken))
        {
            if (!_pausedToasters.Remove(pausedKey))
                return;

            foreach (var toast in _toasts)
            {
                if (toast.ToasterId != normalizedToasterId || toast.Position != position || toast.Removed || toast.Type == SonnerToastType.Loading)
                    continue;

                if (!_timers.TryGetValue(toast.Id, out var timerState) || !timerState.Paused)
                    continue;

                if (timerState.RemainingMs <= 0)
                {
                    _timers.Remove(toast.Id);
                    toastsToDismiss.Add(toast);
                    continue;
                }

                var tokenSource = new CancellationTokenSource();
                timerState.CancellationTokenSource = tokenSource;
                timerState.StartedAt = DateTimeOffset.UtcNow;
                timerState.Paused = false;
                timersToStart.Add((toast, timerState.RemainingMs, tokenSource.Token));
            }
        }

        if (timersToStart.Count != 0)
        {
            for (var index = 0; index < timersToStart.Count; index++)
            {
                var (toast, remainingMs, token) = timersToStart[index];
                _ = AutoDismiss(toast, remainingMs, token);
            }
        }

        if (toastsToDismiss.Count != 0)
        {
            for (var index = 0; index < toastsToDismiss.Count; index++)
            {
                var toast = toastsToDismiss[index];
                _ = DismissCore(toast.Id, invokeAutoClose: true, expectedToast: toast);
            }
        }
    }

    public async ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default)
    {
        if (_disposed.Value)
            return;

        if (id.HasContent())
        {
            await DismissCore(id!, invokeAutoClose: false, cancellationToken);
            return;
        }

        string[] ids;

        using (await _sync.Lock(cancellationToken))
        {
            ids = new string[_toasts.Count];

            for (var i = 0; i < _toasts.Count; i++)
            {
                ids[i] = _toasts[i].Id;
            }
        }

        if (ids.Length == 0) return;
        if (ids.Length == 1)
        {
            await DismissCore(ids[0], invokeAutoClose: false, cancellationToken);
            return;
        }

        var tasks = new Task[ids.Length];

        for (var i = 0; i < ids.Length; i++)
        {
            tasks[i] = DismissCore(ids[i], invokeAutoClose: false, cancellationToken).AsTask();
        }

        await Task.WhenAll(tasks);
    }

    private async ValueTask<string> CreateOrUpdate(string? title, RenderFragment? content, SonnerToastType type, Action<SonnerToastOptions>? configure, CancellationToken cancellationToken,
        string? idOverride = null, string? description = null)
    {
        if (_disposed.Value)
            return string.Empty;

        var options = new SonnerToastOptions { Dismissible = type != SonnerToastType.Loading, Id = idOverride, Description = description };
        configure?.Invoke(options);

        var id = options.Id ?? BlazorIdGenerator.New("quark-sonner-toast");

        SonnerToast? previous;
        string toasterId;
        SonnerToasterRegistration? registration;
        using (await _sync.Lock(cancellationToken))
        {
            previous = FindToast(id);
            toasterId = ResolveToasterId(options.ToasterId, previous?.ToasterId);
            registration = _toasters.GetValueOrDefault(toasterId);
        }

        var duration = options.Duration ?? registration?.DefaultDuration ?? DefaultDuration;
        var closeButton = options.CloseButton ?? registration?.CloseButton ?? DefaultCloseButton;
        var position = options.Position ?? registration?.DefaultPosition ?? DefaultPosition;

        var isExistingToast = previous is not null && !previous.Removed;
        var toast = new SonnerToast(id, previous?.CreatedAt ?? DateTimeOffset.UtcNow)
        {
            ToasterId = toasterId,
            Title = title,
            Description = options.Description,
            Content = options.Content ?? content,
            Type = type,
            Position = position,
            Duration = duration,
            Dismissible = options.Dismissible,
            CloseButton = closeButton,
            ActionLabel = options.ActionLabel,
            Action = options.Action,
            OnDismiss = options.OnDismiss,
            OnAutoClose = options.OnAutoClose,
            Promise = previous?.Promise == true || type == SonnerToastType.Loading,
            Mounted = isExistingToast && previous is not null && previous.Mounted,
            Removed = false
        };

        using (await _sync.Lock(cancellationToken))
        {
            if (_disposed.Value)
                return string.Empty;
            var existingIndex = FindToastIndex(id);

            if (existingIndex >= 0)
                _toasts[existingIndex] = toast;
            else
                _toasts.Add(toast);
        }

        await RestartTimer(toast);
        NotifyStateChanged();

        if (!isExistingToast)
            _ = Mount(id);

        return id;
    }

    private async Task Mount(string id)
    {
        try
        {
            await Task.Delay(_mountDelayMs);

            if (_disposed.Value)
                return;

            bool shouldNotify;

            using (await _sync.Lock())
            {
                if (_disposed.Value)
                    return;

                var toast = FindToast(id);

                if (toast is null || toast.Removed || toast.Mounted)
                    return;

                toast.Mounted = true;
                shouldNotify = true;
            }

            if (shouldNotify)
                NotifyStateChanged();
        }
        catch (TaskCanceledException)
        {
        }
    }

    private async ValueTask RestartTimer(SonnerToast toast)
    {
        CancellationTokenSource? previousSource = null;
        CancellationToken? token = null;
        using (await _sync.Lock())
        {
            if (_disposed.Value || !ReferenceEquals(FindToast(toast.Id), toast) || toast.Removed)
                return;

            if (_timers.Remove(toast.Id, out var previous))
                previousSource = previous.CancellationTokenSource;

            if (toast.Duration > 0 && toast.Type != SonnerToastType.Loading)
            {
                var timerState = new SonnerTimerState
                {
                    RemainingMs = toast.Duration,
                    Paused = _pausedToasters.Contains((toast.ToasterId, toast.Position))
                };
                _timers[toast.Id] = timerState;
                if (!timerState.Paused)
                {
                    var source = new CancellationTokenSource();
                    timerState.CancellationTokenSource = source;
                    timerState.StartedAt = DateTimeOffset.UtcNow;
                    token = source.Token;
                }
            }
        }

        if (previousSource is not null)
        {
            await previousSource.CancelAsync();
            previousSource.Dispose();
        }
        if (token is { } timerToken)
            _ = AutoDismiss(toast, toast.Duration, timerToken);
    }

    private async ValueTask AutoDismiss(SonnerToast toast, int duration, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(duration, cancellationToken);

            if (cancellationToken.IsCancellationRequested || _disposed.Value)
                return;

            await DismissCore(toast.Id, invokeAutoClose: true, timerToken: cancellationToken, expectedToast: toast);
        }
        catch (TaskCanceledException)
        {
        }
    }

    private async ValueTask DismissCore(string id, bool invokeAutoClose, CancellationToken cancellationToken = default, CancellationToken? timerToken = null, SonnerToast? expectedToast = null)
    {
        SonnerToast? toast;
        CancellationTokenSource? timerToCancel = null;

        using (await _sync.Lock(cancellationToken))
        {
            if (_disposed.Value)
                return;

            if (timerToken is { } expectedToken &&
                (expectedToken.IsCancellationRequested || !_timers.TryGetValue(id, out var currentTimer) || currentTimer.Paused ||
                 currentTimer.CancellationTokenSource is not { } currentSource || currentSource.Token != expectedToken))
                return;

            toast = FindToast(id);
            if (toast is null || toast.Removed || (expectedToast is not null && !ReferenceEquals(toast, expectedToast)))
                return;

            toast.Removed = true;
            if (_timers.Remove(id, out var timer))
                timerToCancel = timer.CancellationTokenSource;
        }

        // Once removed, finish the exit animation even if the caller cancels.
        // Abandoning cleanup here would leave an invisible toast retained forever.
        if (timerToCancel is not null)
        {
            await timerToCancel.CancelAsync();
            timerToCancel.Dispose();
        }
        NotifyStateChanged();

        await Task.Delay(_removeDelayMs);

        bool removed;
        using (await _sync.Lock())
        {
            // An update may replace this toast during its exit animation.
            removed = _toasts.Remove(toast);
        }

        if (removed)
            NotifyStateChanged();

        try
        {
            if (invokeAutoClose)
            {
                if (toast.OnAutoClose is not null)
                    await toast.OnAutoClose();
            }
            else if (toast.OnDismiss is not null)
            {
                await toast.OnDismiss();
            }
        }
        catch
        {
        }
    }

    private async ValueTask HandlePromise(ValueTask task, string id, SonnerPromiseOptions options, CancellationToken cancellationToken)
    {
        try
        {
            await task;

            await CreateOrUpdate(options.Success, null, SonnerToastType.Success, options.ConfigureSuccess,
                cancellationToken, id, options.SuccessDescription ?? options.Description);
        }
        catch
        {
            await CreateOrUpdate(options.Error, null, SonnerToastType.Error, options.ConfigureError,
                cancellationToken, id, options.ErrorDescription ?? options.Description);
        }
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        DisposeAsync().AsTask().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        string[] ids;

        if (!_disposed.TrySetTrue())
            return;

        using (await _sync.Lock())
        {
            ids = new string[_timers.Count];
            var index = 0;

            foreach (var id in _timers.Keys)
            {
                ids[index++] = id;
            }

            _toasts.Clear();
        }

        foreach (var id in ids)
        {
            await CancelTimer(id);
        }
    }

    private async ValueTask CancelTimer(string id, CancellationToken cancellationToken = default)
    {
        CancellationTokenSource? cts = null;

        using (await _sync.Lock(cancellationToken))
        {
            if (_timers.Remove(id, out var existing))
                cts = existing.CancellationTokenSource;
        }

        if (cts is null)
            return;

        await cts.CancelAsync();
        cts.Dispose();
    }

    private void NotifyStateChanged()
    {
        StateChanged?.Invoke();
    }

    // The caller holds _sync while resolving registration defaults.
    private string ResolveToasterId(string? requestedToasterId, string? previousToasterId)
    {
        if (requestedToasterId.HasContent()) return NormalizeToasterId(requestedToasterId);
        if (previousToasterId.HasContent()) return NormalizeToasterId(previousToasterId);
        if (_toasters.ContainsKey(_defaultToasterId)) return _defaultToasterId;
        if (_toasters.ContainsKey(_activeToasterId)) return _activeToasterId;
        return FirstToasterIdOrDefault();
    }

    private static string NormalizeToasterId(string? toasterId)
    {
        return string.IsNullOrWhiteSpace(toasterId) ? _defaultToasterId : toasterId;
    }

    private static int GetRemainingMs(SonnerTimerState timerState, DateTimeOffset now)
    {
        var elapsedMs = (int)Math.Ceiling((now - timerState.StartedAt).TotalMilliseconds);
        var remainingMs = timerState.RemainingMs - elapsedMs;
        return remainingMs > 0 ? remainingMs : 0;
    }

    private SonnerToast? FindToast(string id)
    {
        var index = FindToastIndex(id);
        return index < 0 ? null : _toasts[index];
    }

    private int FindToastIndex(string id)
    {
        for (var index = 0; index < _toasts.Count; index++)
            if (_toasts[index].Id == id) return index;
        return -1;
    }

    private string FirstToasterIdOrDefault()
    {
        foreach (var toasterId in _toasters.Keys)
        {
            return toasterId;
        }

        return _defaultToasterId;
    }
}
