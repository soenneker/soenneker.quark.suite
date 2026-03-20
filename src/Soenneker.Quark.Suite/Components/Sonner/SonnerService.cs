using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public sealed class SonnerService : ISonnerService, IDisposable
{
    private const int RemoveDelayMs = 400;
    private const string DefaultToasterId = "";
    private readonly object _sync = new();
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

    public IReadOnlyList<SonnerToast> GetToasts()
    {
        lock (_sync)
        {
            return _toasts.OrderBy(toast => toast.CreatedAt)
                         .ToArray();
        }
    }

    public string Toast(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Default, configure);
    }

    public string Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(null, content, SonnerToastType.Default, configure);
    }

    public string Success(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Success, configure);
    }

    public string Info(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Info, configure);
    }

    public string Warning(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Warning, configure);
    }

    public string Error(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Error, configure);
    }

    public string Loading(string title, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(title, null, SonnerToastType.Loading, options =>
        {
            options.Dismissible = false;
            configure?.Invoke(options);
        });
    }

    public string Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return CreateOrUpdate(null, content, SonnerToastType.Default, configure);
    }

    public string Promise(Task task, SonnerPromiseOptions options)
    {
        var id = Loading(options.Loading, toastOptions =>
        {
            toastOptions.Description = options.Description;
            options.ConfigureLoading?.Invoke(toastOptions);
        });

        lock (_sync)
        {
            var existing = _toasts.FirstOrDefault(toast => toast.Id == id);
            if (existing is not null)
                existing.Promise = true;
        }

        _ = HandlePromiseAsync(task, id, options);
        return id;
    }

    public string Promise(Func<Task> taskFactory, SonnerPromiseOptions options)
    {
        return Promise(taskFactory(), options);
    }

    public void RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton)
    {
        var normalizedToasterId = NormalizeToasterId(toasterId);

        lock (_sync)
        {
            _toasters[normalizedToasterId] = new ToasterRegistration(defaultPosition, defaultDuration, closeButton);
            _activeToasterId = normalizedToasterId;
        }
    }

    public void UnregisterToaster(string? toasterId)
    {
        var normalizedToasterId = NormalizeToasterId(toasterId);

        lock (_sync)
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

    public void Pause(string? toasterId, SonnerPosition position)
    {
        if (_disposed)
            return;

        List<CancellationTokenSource> timersToCancel = [];
        var now = DateTimeOffset.UtcNow;
        var normalizedToasterId = NormalizeToasterId(toasterId);
        var pausedKey = (normalizedToasterId, position);

        lock (_sync)
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

    public void Resume(string? toasterId, SonnerPosition position)
    {
        if (_disposed)
            return;

        List<(string Id, int RemainingMs, CancellationTokenSource TokenSource)> timersToStart = [];
        List<string> toastsToDismiss = [];
        var normalizedToasterId = NormalizeToasterId(toasterId);
        var pausedKey = (normalizedToasterId, position);

        lock (_sync)
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
            _ = AutoDismissAsync(id, remainingMs, tokenSource.Token);
        }

        foreach (var id in toastsToDismiss)
        {
            _ = DismissAsync(id, invokeAutoClose: true);
        }
    }

    public void Dismiss(string? id = null)
    {
        if (_disposed)
            return;

        if (!string.IsNullOrWhiteSpace(id))
        {
            _ = DismissAsync(id!, invokeAutoClose: false);
            return;
        }

        string[] ids;

        lock (_sync)
        {
            ids = _toasts.Select(toast => toast.Id)
                         .ToArray();
        }

        foreach (var toastId in ids)
        {
            _ = DismissAsync(toastId, invokeAutoClose: false);
        }
    }

    private string CreateOrUpdate(string? title, RenderFragment? content, SonnerToastType type, Action<SonnerToastOptions>? configure)
    {
        if (_disposed)
            return string.Empty;

        var options = new SonnerToastOptions();
        configure?.Invoke(options);

        var id = options.Id ?? Guid.NewGuid()
                               .ToString("N");

        SonnerToast? previous = null;

        lock (_sync)
        {
            previous = _toasts.FirstOrDefault(item => item.Id == id);
        }

        var toasterId = ResolveToasterId(options.ToasterId, previous?.ToasterId);
        var registration = GetToasterRegistration(toasterId);
        var duration = options.Duration ?? registration?.DefaultDuration ?? DefaultDuration;
        var closeButton = options.CloseButton ?? registration?.CloseButton ?? DefaultCloseButton;
        var position = options.Position ?? registration?.DefaultPosition ?? DefaultPosition;

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
            Mounted = true,
            Removed = false,
            CreatedAt = previous?.CreatedAt ?? DateTimeOffset.UtcNow
        };

        lock (_sync)
        {
            var existingIndex = _toasts.FindIndex(item => item.Id == id);

            if (existingIndex >= 0)
                _toasts[existingIndex] = toast;
            else
                _toasts.Add(toast);
        }

        RestartTimer(toast);
        NotifyStateChanged();

        return id;
    }

    private void RestartTimer(SonnerToast toast)
    {
        CancelTimer(toast.Id);

        if (toast.Duration <= 0 || toast.Type == SonnerToastType.Loading)
            return;

        CancellationTokenSource? tokenSource = null;

        lock (_sync)
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
            _ = AutoDismissAsync(toast.Id, toast.Duration, tokenSource.Token);
    }

    private async Task AutoDismissAsync(string id, int duration, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(duration, cancellationToken)
                      .ConfigureAwait(false);

            if (cancellationToken.IsCancellationRequested || _disposed)
                return;

            await DismissAsync(id, invokeAutoClose: true).ConfigureAwait(false);
        }
        catch (TaskCanceledException)
        {
        }
    }

    private async Task DismissAsync(string id, bool invokeAutoClose)
    {
        SonnerToast? toast;

        lock (_sync)
        {
            if (_disposed)
                return;

            toast = _toasts.FirstOrDefault(item => item.Id == id);
            if (toast is null || toast.Removed)
                return;

            toast.Removed = true;
        }

        CancelTimer(id);
        NotifyStateChanged();

        await Task.Delay(RemoveDelayMs).ConfigureAwait(false);

        if (toast is null)
            return;

        lock (_sync)
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
                    await toast.OnAutoClose().ConfigureAwait(false);
            }
            else if (toast.OnDismiss is not null)
            {
                await toast.OnDismiss().ConfigureAwait(false);
            }
        }
        catch
        {
        }
    }

    private static async Task InvokeActionAsync(Func<ValueTask>? callback)
    {
        if (callback is not null)
            await callback().ConfigureAwait(false);
    }

    private async Task HandlePromiseAsync(Task task, string id, SonnerPromiseOptions options)
    {
        try
        {
            await task.ConfigureAwait(false);

            CreateOrUpdate(options.Success, null, SonnerToastType.Success, toastOptions =>
            {
                toastOptions.Id = id;
                toastOptions.Description = options.SuccessDescription ?? options.Description;
                options.ConfigureSuccess?.Invoke(toastOptions);
            });
        }
        catch
        {
            CreateOrUpdate(options.Error, null, SonnerToastType.Error, toastOptions =>
            {
                toastOptions.Id = id;
                toastOptions.Description = options.ErrorDescription ?? options.Description;
                options.ConfigureError?.Invoke(toastOptions);
            });
        }
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;

        string[] ids;

        lock (_sync)
        {
            ids = _timers.Keys.ToArray();
            _toasts.Clear();
        }

        foreach (var id in ids)
        {
            CancelTimer(id);
        }
    }

    private void CancelTimer(string id)
    {
        CancellationTokenSource? cts = null;

        lock (_sync)
        {
            if (_timers.Remove(id, out var existing))
                cts = existing.CancellationTokenSource;
        }

        if (cts is null)
            return;

        cts.Cancel();
        cts.Dispose();
    }

    private void NotifyStateChanged()
    {
        StateChanged?.Invoke();
    }

    private ToasterRegistration? GetToasterRegistration(string toasterId)
    {
        lock (_sync)
        {
            return _toasters.GetValueOrDefault(toasterId);
        }
    }

    private string ResolveToasterId(string? requestedToasterId, string? previousToasterId = null)
    {
        if (!string.IsNullOrWhiteSpace(requestedToasterId))
            return NormalizeToasterId(requestedToasterId);

        if (!string.IsNullOrWhiteSpace(previousToasterId))
            return NormalizeToasterId(previousToasterId);

        lock (_sync)
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
