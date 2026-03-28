using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Asyncs.Locks;

namespace Soenneker.Quark;

///<inheritdoc cref="ISonnerService"/>
public sealed class SonnerService : ISonnerService
{
    private const int MountDelayMs = 16;
    private const int RemoveDelayMs = 400;
    private const string DefaultToasterId = "";
    private readonly AsyncLock _sync = new();
    private readonly List<SonnerToast> _toasts = [];
    private readonly Dictionary<string, TimerState> _timers = new(StringComparer.Ordinal);
    private readonly Dictionary<string, ToasterRegistration> _toasters = new(StringComparer.Ordinal);
    private readonly HashSet<(string ToasterId, SonnerPosition Position)> _pausedToasters = [];
    private string _activeToasterId = DefaultToasterId;
    private bool _disposed;

    public event Action? StateChanged;

    public SonnerPosition DefaultPosition { get; set; } = SonnerPosition.TopCenter;

    public int DefaultDuration { get; set; } = 4000;

    public bool DefaultCloseButton { get; set; }

    public async ValueTask<IReadOnlyList<SonnerToast>> GetToasts()
    {
        using (await _sync.Lock())
        {
            return _toasts.OrderBy(toast => toast.CreatedAt)
                         .ToArray();
        }
    }

    public ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Default, configure);
    }

    public ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(null, content, SonnerToastType.Default, configure);
    }

    public ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Success, configure);
    }

    public ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Info, configure);
    }

    public ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Warning, configure);
    }

    public ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Error, configure);
    }

    public ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Loading, options =>
        {
            options.Dismissible = false;
            configure?.Invoke(options);
        });
    }

    public ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(null, content, SonnerToastType.Default, configure);
    }

    public async ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options)
    {
        var id = await Loading(options.Loading, toastOptions =>
        {
            toastOptions.Description = options.Description;
            options.ConfigureLoading?.Invoke(toastOptions);
        });

        using (await _sync.Lock())
        {
            var existing = _toasts.FirstOrDefault(toast => toast.Id == id);
            if (existing is not null)
                existing.Promise = true;
        }

        _ = HandlePromise(task, id, options);
        return id;
    }

    public ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options)
    {
        return Promise(taskFactory(), options);
    }

    public async ValueTask RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton)
    {
        var normalizedToasterId = NormalizeToasterId(toasterId);

        using (await _sync.Lock())
        {
            _toasters[normalizedToasterId] = new ToasterRegistration(defaultPosition, defaultDuration, closeButton);
            _activeToasterId = normalizedToasterId;
        }
    }

    public async ValueTask UnregisterToaster(string? toasterId)
    {
        var normalizedToasterId = NormalizeToasterId(toasterId);

        using (await _sync.Lock())
        {
            _toasters.Remove(normalizedToasterId);

            foreach (var pausedToaster in _pausedToasters.Where(item => item.ToasterId == normalizedToasterId)
                                                         .ToArray())
            {
                _pausedToasters.Remove(pausedToaster);
            }

            if (_activeToasterId == normalizedToasterId)
            {
                if (_toasters.ContainsKey(DefaultToasterId))
                    _activeToasterId = DefaultToasterId;
                else
                    _activeToasterId = _toasters.Keys.FirstOrDefault() ?? DefaultToasterId;
            }
        }
    }

    public async ValueTask Pause(string? toasterId, SonnerPosition position)
    {
        if (_disposed)
            return;

        List<CancellationTokenSource> timersToCancel = [];
        var now = DateTimeOffset.UtcNow;
        var normalizedToasterId = NormalizeToasterId(toasterId);
        var pausedKey = (normalizedToasterId, position);

        using (await _sync.Lock())
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

        foreach (var timer in timersToCancel)
        {
            timer.Cancel();
            timer.Dispose();
        }
    }

    public async ValueTask Resume(string? toasterId, SonnerPosition position)
    {
        if (_disposed)
            return;

        List<(string Id, int RemainingMs, CancellationTokenSource TokenSource)> timersToStart = [];
        List<string> toastsToDismiss = [];
        var normalizedToasterId = NormalizeToasterId(toasterId);
        var pausedKey = (normalizedToasterId, position);

        using (await _sync.Lock())
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
                    toastsToDismiss.Add(toast.Id);
                    continue;
                }

                var tokenSource = new CancellationTokenSource();
                timerState.CancellationTokenSource = tokenSource;
                timerState.StartedAt = DateTimeOffset.UtcNow;
                timerState.Paused = false;
                timersToStart.Add((toast.Id, timerState.RemainingMs, tokenSource));
            }
        }

        foreach ((string id, int remainingMs, CancellationTokenSource tokenSource) in timersToStart)
        {
            _ = AutoDismiss(id, remainingMs, tokenSource.Token);
        }

        foreach (var id in toastsToDismiss)
        {
            _ = DismissCore(id, invokeAutoClose: true);
        }
    }

    public async ValueTask Dismiss(string? id = null)
    {
        if (_disposed)
            return;

        if (!string.IsNullOrWhiteSpace(id))
        {
            await DismissCore(id!, invokeAutoClose: false);
            return;
        }

        string[] ids;

        using (await _sync.Lock())
        {
            ids = _toasts.Select(toast => toast.Id)
                         .ToArray();
        }

        await Task.WhenAll(ids.Select(toastId => DismissCore(toastId, invokeAutoClose: false).AsTask()));
    }

    private async ValueTask<string> CreateOrUpdate(string? title, RenderFragment? content, SonnerToastType type, Action<SonnerToastOptions>? configure)
    {
        if (_disposed)
            return string.Empty;

        var options = new SonnerToastOptions();
        configure?.Invoke(options);

        var id = options.Id ?? Guid.NewGuid()
                               .ToString("N");

        SonnerToast? previous = null;

        using (await _sync.Lock())
        {
            previous = _toasts.FirstOrDefault(item => item.Id == id);
        }

        var toasterId = await ResolveToasterId(options.ToasterId, previous?.ToasterId);
        var registration = await GetToasterRegistration(toasterId);
        var duration = options.Duration ?? registration?.DefaultDuration ?? DefaultDuration;
        var closeButton = options.CloseButton ?? registration?.CloseButton ?? DefaultCloseButton;
        var position = options.Position ?? registration?.DefaultPosition ?? DefaultPosition;

        var isExistingToast = previous is not null && !previous.Removed;
        var toast = new SonnerToast
        {
            ToasterId = toasterId,
            Id = id,
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
            Removed = false,
            CreatedAt = previous?.CreatedAt ?? DateTimeOffset.UtcNow
        };

        using (await _sync.Lock())
        {
            var existingIndex = _toasts.FindIndex(item => item.Id == id);

            if (existingIndex >= 0)
                _toasts[existingIndex] = toast;
            else
                _toasts.Add(toast);
        }

        await RestartTimer(toast);
        NotifyStateChanged();

        if (!isExistingToast)
            _ = MountAsync(id);

        return id;
    }

    private async Task MountAsync(string id)
    {
        try
        {
            await Task.Delay(MountDelayMs);

            if (_disposed)
                return;

            var shouldNotify = false;

            using (await _sync.Lock())
            {
                if (_disposed)
                    return;

                var toast = _toasts.FirstOrDefault(item => item.Id == id);

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
        await CancelTimer(toast.Id);

        if (toast.Duration <= 0 || toast.Type == SonnerToastType.Loading)
            return;

        CancellationTokenSource? tokenSource = null;

        using (await _sync.Lock())
        {
            var timerState = new TimerState
            {
                RemainingMs = toast.Duration,
                Paused = _pausedToasters.Contains((toast.ToasterId, toast.Position))
            };

            _timers[toast.Id] = timerState;

            if (timerState.Paused)
                return;

            tokenSource = new CancellationTokenSource();
            timerState.CancellationTokenSource = tokenSource;
            timerState.StartedAt = DateTimeOffset.UtcNow;
        }

        if (tokenSource is not null)
            _ = AutoDismiss(toast.Id, toast.Duration, tokenSource.Token);
    }

    private async ValueTask AutoDismiss(string id, int duration, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(duration, cancellationToken);

            if (cancellationToken.IsCancellationRequested || _disposed)
                return;

            await DismissCore(id, invokeAutoClose: true);
        }
        catch (TaskCanceledException)
        {
        }
    }

    private async ValueTask DismissCore(string id, bool invokeAutoClose)
    {
        SonnerToast? toast;

        using (await _sync.Lock())
        {
            if (_disposed)
                return;

            toast = _toasts.FirstOrDefault(item => item.Id == id);
            if (toast is null || toast.Removed)
                return;

            toast.Removed = true;
        }

        await CancelTimer(id);
        NotifyStateChanged();

        await Task.Delay(RemoveDelayMs);

        if (toast is null)
            return;

        using (await _sync.Lock())
        {
            var index = _toasts.FindIndex(item => item.Id == id);
            if (index >= 0)
                _toasts.RemoveAt(index);
        }

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

    private async ValueTask HandlePromise(ValueTask task, string id, SonnerPromiseOptions options)
    {
        try
        {
            await task;

            await CreateOrUpdate(options.Success, null, SonnerToastType.Success, toastOptions =>
            {
                toastOptions.Id = id;
                toastOptions.Description = options.SuccessDescription ?? options.Description;
                options.ConfigureSuccess?.Invoke(toastOptions);
            });
        }
        catch
        {
            await CreateOrUpdate(options.Error, null, SonnerToastType.Error, toastOptions =>
            {
                toastOptions.Id = id;
                toastOptions.Description = options.ErrorDescription ?? options.Description;
                options.ConfigureError?.Invoke(toastOptions);
            });
        }
    }

    public void Dispose()
    {
        DisposeAsync().AsTask().GetAwaiter().GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        string[] ids;

        using (await _sync.Lock())
        {
            if (_disposed)
                return;

            _disposed = true;
            ids = _timers.Keys.ToArray();
            _toasts.Clear();
        }

        foreach (var id in ids)
        {
            await CancelTimer(id);
        }
    }

    private async ValueTask CancelTimer(string id)
    {
        CancellationTokenSource? cts = null;

        using (await _sync.Lock())
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

    private async ValueTask<ToasterRegistration?> GetToasterRegistration(string toasterId)
    {
        using (await _sync.Lock())
        {
            return _toasters.GetValueOrDefault(toasterId);
        }
    }

    private async ValueTask<string> ResolveToasterId(string? requestedToasterId, string? previousToasterId = null)
    {
        if (!string.IsNullOrWhiteSpace(requestedToasterId))
            return NormalizeToasterId(requestedToasterId);

        if (!string.IsNullOrWhiteSpace(previousToasterId))
            return NormalizeToasterId(previousToasterId);

        using (await _sync.Lock())
        {
            if (_toasters.ContainsKey(DefaultToasterId))
                return DefaultToasterId;

            if (_toasters.ContainsKey(_activeToasterId))
                return _activeToasterId;

            return _toasters.Keys.FirstOrDefault() ?? DefaultToasterId;
        }
    }

    private static string NormalizeToasterId(string? toasterId)
    {
        return string.IsNullOrWhiteSpace(toasterId) ? DefaultToasterId : toasterId;
    }

    private static int GetRemainingMs(TimerState timerState, DateTimeOffset now)
    {
        var elapsedMs = (int)Math.Ceiling((now - timerState.StartedAt).TotalMilliseconds);
        var remainingMs = timerState.RemainingMs - elapsedMs;
        return remainingMs > 0 ? remainingMs : 0;
    }

    private sealed class TimerState
    {
        public CancellationTokenSource? CancellationTokenSource { get; set; }

        public DateTimeOffset StartedAt { get; set; }

        public int RemainingMs { get; set; }

        public bool Paused { get; set; }
    }

    private sealed record ToasterRegistration(SonnerPosition? DefaultPosition, int? DefaultDuration, bool? CloseButton);
}
