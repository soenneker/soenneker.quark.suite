using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Soenneker.Quark;

/// <inheritdoc cref="IQuarkDateTimeScheduler"/>
public sealed class QuarkDateTimeScheduler : IQuarkDateTimeScheduler
{
    private readonly object _sync = new();
    private readonly HashSet<QuarkDateTimeScheduleRegistration> _registrations = [];
    private readonly CancellationTokenSource _disposeCts = new();
    private readonly ILogger<QuarkDateTimeScheduler> _logger;
    private CancellationTokenSource _wakeCts = new();
    private Task? _runner;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the scheduler.
    /// </summary>
    public QuarkDateTimeScheduler(ILogger<QuarkDateTimeScheduler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
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

            if (registration.NextUpdate is not null)
                _runner ??= Run();

            WakeRunnerCore();
        }

        return registration;
    }

    private async Task Run()
    {
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

                DateTimeOffset now = DateTimeOffset.UtcNow;
                DateTimeOffset? earliest = null;

                foreach (QuarkDateTimeScheduleRegistration registration in _registrations)
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

            List<(QuarkDateTimeScheduleRegistration Registration, int Version)> due = [];
            DateTimeOffset tickNow = DateTimeOffset.UtcNow;

            lock (_sync)
            {
                foreach (QuarkDateTimeScheduleRegistration registration in _registrations)
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
                (QuarkDateTimeScheduleRegistration registration, int version) = due[i];

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

            if (registration.NextUpdate is not null)
                _runner ??= Run();

            WakeRunnerCore();
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
        TimeSpan? interval = registration.GetNextInterval(now);
        registration.NextUpdate = interval.HasValue && interval.Value > TimeSpan.Zero ? now + interval.Value : null;
    }

    private void WakeRunnerCore()
    {
        CancellationTokenSource previous = _wakeCts;
        _wakeCts = CancellationTokenSource.CreateLinkedTokenSource(_disposeCts.Token);
        previous.Cancel();
        previous.Dispose();
    }

    /// <inheritdoc />
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
