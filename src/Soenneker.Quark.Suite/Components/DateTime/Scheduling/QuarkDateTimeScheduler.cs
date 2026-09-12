using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Soenneker.Quark;

public sealed class QuarkDateTimeScheduler : IQuarkDateTimeScheduler
{
    private readonly object _sync = new();
    private readonly HashSet<QuarkDateTimeScheduleRegistration> _registrations = [];
    private readonly CancellationTokenSource _disposeCts = new();
    private readonly ILogger<QuarkDateTimeScheduler> _logger;
    private CancellationTokenSource _wakeCts = new();
    private Task? _runner;
    private bool _disposed;

    public QuarkDateTimeScheduler(ILogger<QuarkDateTimeScheduler> logger)
    {
        _logger = logger;
    }

    public IQuarkDateTimeScheduleRegistration Register(Func<DateTimeOffset, TimeSpan?> getNextInterval,
        Func<DateTimeOffset, ValueTask> callback)
    {
        ArgumentNullException.ThrowIfNull(getNextInterval);
        ArgumentNullException.ThrowIfNull(callback);

        var registration = new QuarkDateTimeScheduleRegistration(this, getNextInterval, callback);

        lock (_sync)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _registrations.Add(registration);
            ScheduleCore(registration, DateTimeOffset.UtcNow);

            WakeRunnerCore();

            if (registration.NextUpdate is not null)
                _runner ??= Run();
        }

        return registration;
    }

    private async Task Run()
    {
        // Publish the runner before it can invoke callbacks that reenter the scheduler.
        await Task.Yield();
        List<(QuarkDateTimeScheduleRegistration Registration, int Version)> due = [];

        while (true)
        {
            TimeSpan delay;
            CancellationToken wakeToken;

            lock (_sync)
            {
                if (_disposed || _registrations.Count == 0)
                {
                    _runner = null;
                    return;
                }

                var now = DateTimeOffset.UtcNow;
                DateTimeOffset? earliest = null;

                foreach (var registration in _registrations)
                {
                    if (registration.NextUpdate is not null && (!earliest.HasValue || registration.NextUpdate.Value < earliest.Value))
                        earliest = registration.NextUpdate;
                }

                if (!earliest.HasValue)
                {
                    _runner = null;
                    return;
                }

                delay = earliest.Value <= now ? TimeSpan.Zero : earliest.Value - now;
                if (_wakeCts.IsCancellationRequested)
                {
                    _wakeCts.Dispose();
                    _wakeCts = new CancellationTokenSource();
                }
                wakeToken = _wakeCts.Token;
            }

            try
            {
                await Task.Delay(delay, wakeToken);
            }
            catch (OperationCanceledException) when (!_disposeCts.IsCancellationRequested)
            {
                continue;
            }
            catch (OperationCanceledException)
            {
                return;
            }

            var tickNow = DateTimeOffset.UtcNow;

            lock (_sync)
            {
                foreach (var registration in _registrations)
                {
                    if (registration.NextUpdate is not null && registration.NextUpdate.Value <= tickNow)
                    {
                        registration.NextUpdate = null;
                        due.Add((registration, registration.Version));
                    }
                }
            }

            for (var i = 0; i < due.Count; i++)
            {
                (var registration, var version) = due[i];

                lock (_sync)
                {
                    if (registration.Disposed || registration.Version != version || !_registrations.Contains(registration))
                        continue;
                }

                try
                {
                    await registration.Callback(tickNow);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Quark date/time scheduled update failed.");
                }

                lock (_sync)
                {
                    if (!registration.Disposed && registration.Version == version && _registrations.Contains(registration))
                        ScheduleCore(registration, DateTimeOffset.UtcNow);
                }
            }

            // Retain capacity for the next tick without retaining component callbacks during the delay.
            due.Clear();
        }
    }

    internal void Reschedule(QuarkDateTimeScheduleRegistration registration)
    {
        lock (_sync)
        {
            if (_disposed || registration.Disposed || !_registrations.Contains(registration))
                return;

            registration.Version++;
            ScheduleCore(registration, DateTimeOffset.UtcNow);

            WakeRunnerCore();

            if (registration.NextUpdate is not null)
                _runner ??= Run();
        }
    }

    internal void Unregister(QuarkDateTimeScheduleRegistration registration)
    {
        lock (_sync)
        {
            if (registration.Disposed)
                return;

            registration.Disposed = true;
            registration.Version++;
            registration.NextUpdate = null;
            _registrations.Remove(registration);
            WakeRunnerCore();
        }
    }

    private static void ScheduleCore(QuarkDateTimeScheduleRegistration registration, DateTimeOffset now)
    {
        var interval = registration.GetNextInterval(now);
        registration.NextUpdate = interval.HasValue && interval.Value > TimeSpan.Zero ? now + interval.Value : null;
    }

    private void WakeRunnerCore()
    {
        // A burst of registrations needs one wake-up; the runner replaces the source when it waits again.
        if (!_disposed && _runner is not null)
            _wakeCts.Cancel();
    }

    public async ValueTask DisposeAsync()
    {
        Task? runner;

        lock (_sync)
        {
            if (_disposed)
                return;

            _disposed = true;
            _registrations.Clear();
            _disposeCts.Cancel();
            _wakeCts.Cancel();
            runner = _runner;
        }

        if (runner is not null)
        {
            try
            {
                await runner;
            }
            catch (OperationCanceledException)
            {
            }
        }

        _wakeCts.Dispose();
        _disposeCts.Dispose();
    }
}
