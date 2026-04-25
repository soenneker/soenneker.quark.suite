using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Asyncs.Locks;
using Soenneker.Blazor.Utils.Ids;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

///<inheritdoc cref="ISonnerService"/>
public sealed class SonnerService : ISonnerService
{
    private const int _mountDelayMs = 16;
    private const int _removeDelayMs = 400;
    private const string _defaultToasterId = "";
    private readonly AsyncLock _sync = new();
    private readonly List<SonnerToast> _toasts = [];
    private readonly Dictionary<string, TimerState> _timers = new(StringComparer.Ordinal);
    private readonly Dictionary<string, ToasterRegistration> _toasters = new(StringComparer.Ordinal);
    private readonly HashSet<(string ToasterId, SonnerPosition Position)> _pausedToasters = [];
    private string _activeToasterId = _defaultToasterId;
    private bool _disposed;

    public event Action? StateChanged;

    public SonnerPosition DefaultPosition { get; set; } = SonnerPosition.TopCenter;

    public int DefaultDuration { get; set; } = 4000;

    public bool DefaultCloseButton { get; set; }

    public async ValueTask<IReadOnlyList<SonnerToast>> GetToasts(CancellationToken cancellationToken = default)
    {
        using (await _sync.Lock(cancellationToken))
        {
            return _toasts.OrderBy(toast => toast.CreatedAt)
                         .ToArray();
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
        return CreateOrUpdate(title, null, SonnerToastType.Loading, options =>
        {
            options.Dismissible = false;
            configure?.Invoke(options);
        }, cancellationToken);
    }

    public ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return CreateOrUpdate(null, content, SonnerToastType.Default, configure, cancellationToken);
    }

    public async ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options, CancellationToken cancellationToken)
    {
        var id = await Loading(options.Loading, toastOptions =>
        {
            toastOptions.Description = options.Description;
            options.ConfigureLoading?.Invoke(toastOptions);
        }, cancellationToken);

        using (await _sync.Lock(cancellationToken))
        {
            var existing = _toasts.FirstOrDefault(toast => toast.Id == id);
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
            _toasters[normalizedToasterId] = new ToasterRegistration(defaultPosition, defaultDuration, closeButton);
            _activeToasterId = normalizedToasterId;
        }
    }

    public async ValueTask UnregisterToaster(string? toasterId, CancellationToken cancellationToken = default)
    {
        var normalizedToasterId = NormalizeToasterId(toasterId);

        using (await _sync.Lock(cancellationToken))
        {
            _toasters.Remove(normalizedToasterId);

            foreach (var pausedToaster in _pausedToasters.Where(item => item.ToasterId == normalizedToasterId)
                                                         .ToArray())
            {
                _pausedToasters.Remove(pausedToaster);
            }

            if (_activeToasterId == normalizedToasterId)
            {
                if (_toasters.ContainsKey(_defaultToasterId))
                    _activeToasterId = _defaultToasterId;
                else
                    _activeToasterId = _toasters.Keys.FirstOrDefault() ?? _defaultToasterId;
            }
        }
    }

    public async ValueTask Pause(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default)
    {
        if (_disposed)
            return;

        List<CancellationTokenSource> timersToCancel = [];
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

        foreach (var timer in timersToCancel)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await timer.CancelAsync();
            timer.Dispose();
        }
    }

    public async ValueTask Resume(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default)
    {
        if (_disposed)
            return;

        List<(string Id, int RemainingMs, CancellationTokenSource TokenSource)> timersToStart = [];
        List<string> toastsToDismiss = [];
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

        foreach ((var id, var remainingMs, var tokenSource) in timersToStart)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _ = AutoDismiss(id, remainingMs, tokenSource.Token);
        }

        foreach (var id in toastsToDismiss)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _ = DismissCore(id, invokeAutoClose: true, cancellationToken);
        }
    }

    public async ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default)
    {
        if (_disposed)
            return;

        if (id.HasContent())
        {
            await DismissCore(id!, invokeAutoClose: false, cancellationToken);
            return;
        }

        string[] ids;

        using (await _sync.Lock(cancellationToken))
        {
            ids = _toasts.Select(toast => toast.Id)
                         .ToArray();
        }

        await Task.WhenAll(ids.Select(toastId => DismissCore(toastId, invokeAutoClose: false, cancellationToken).AsTask()));
    }

    private async ValueTask<string> CreateOrUpdate(string? title, RenderFragment? content, SonnerToastType type, Action<SonnerToastOptions>? configure, CancellationToken cancellationToken)
    {
        if (_disposed)
            return string.Empty;

        var options = new SonnerToastOptions();
        configure?.Invoke(options);

        var id = options.Id ?? BlazorIdGenerator.New("quark-sonner-toast");

        SonnerToast? previous = null;

        using (await _sync.Lock(cancellationToken))
        {
            previous = _toasts.FirstOrDefault(item => item.Id == id);
        }

        var toasterId = await ResolveToasterId(options.ToasterId, previous?.ToasterId, cancellationToken);
        var registration = await GetToasterRegistration(toasterId, cancellationToken);
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

        using (await _sync.Lock(cancellationToken))
        {
            var existingIndex = _toasts.FindIndex(item => item.Id == id);

            if (existingIndex >= 0)
                _toasts[existingIndex] = toast;
            else
                _toasts.Add(toast);
        }

        await RestartTimer(toast, cancellationToken);
        NotifyStateChanged();

        if (!isExistingToast)
            _ = MountAsync(id);

        return id;
    }

    private async Task MountAsync(string id)
    {
        try
        {
            await Task.Delay(_mountDelayMs);

            if (_disposed)
                return;

            bool shouldNotify;

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

    private async ValueTask RestartTimer(SonnerToast toast, CancellationToken cancellationToken)
    {
        await CancelTimer(toast.Id, cancellationToken);

        if (toast.Duration <= 0 || toast.Type == SonnerToastType.Loading)
            return;

        CancellationTokenSource? tokenSource = null;

        using (await _sync.Lock(cancellationToken))
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

    private async ValueTask DismissCore(string id, bool invokeAutoClose, CancellationToken cancellationToken = default)
    {
        SonnerToast? toast;

        using (await _sync.Lock(cancellationToken))
        {
            if (_disposed)
                return;

            toast = _toasts.FirstOrDefault(item => item.Id == id);
            if (toast is null || toast.Removed)
                return;

            toast.Removed = true;
        }

        await CancelTimer(id, cancellationToken);
        NotifyStateChanged();

        await Task.Delay(_removeDelayMs, cancellationToken);

        if (toast is null)
            return;

        using (await _sync.Lock(cancellationToken))
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

    private async ValueTask HandlePromise(ValueTask task, string id, SonnerPromiseOptions options, CancellationToken cancellationToken)
    {
        try
        {
            await task;

            await CreateOrUpdate(options.Success, null, SonnerToastType.Success, toastOptions =>
            {
                toastOptions.Id = id;
                toastOptions.Description = options.SuccessDescription ?? options.Description;
                options.ConfigureSuccess?.Invoke(toastOptions);
            }, cancellationToken);
        }
        catch
        {
            await CreateOrUpdate(options.Error, null, SonnerToastType.Error, toastOptions =>
            {
                toastOptions.Id = id;
                toastOptions.Description = options.ErrorDescription ?? options.Description;
                options.ConfigureError?.Invoke(toastOptions);
            }, cancellationToken);
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

    private async ValueTask<ToasterRegistration?> GetToasterRegistration(string toasterId, CancellationToken cancellationToken)
    {
        using (await _sync.Lock(cancellationToken))
        {
            return _toasters.GetValueOrDefault(toasterId);
        }
    }

    private async ValueTask<string> ResolveToasterId(string? requestedToasterId, string? previousToasterId = null, CancellationToken cancellationToken = default)
    {
        if (requestedToasterId.HasContent())
            return NormalizeToasterId(requestedToasterId);

        if (previousToasterId.HasContent())
            return NormalizeToasterId(previousToasterId);

        using (await _sync.Lock(cancellationToken))
        {
            if (_toasters.ContainsKey(_defaultToasterId))
                return _defaultToasterId;

            if (_toasters.ContainsKey(_activeToasterId))
                return _activeToasterId;

            return _toasters.Keys.FirstOrDefault() ?? _defaultToasterId;
        }
    }

    private static string NormalizeToasterId(string? toasterId)
    {
        return string.IsNullOrWhiteSpace(toasterId) ? _defaultToasterId : toasterId;
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
