using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Soenneker.Quark;

/// <inheritdoc cref="IQuarkBrowserTimeZoneService"/>
public sealed class QuarkBrowserTimeZoneService : IQuarkBrowserTimeZoneService
{
    private readonly IQuarkBrowserTimeZoneInterop _interop;
    private readonly ILogger<QuarkBrowserTimeZoneService> _logger;
    private readonly object _sync = new();
    private Task<string?>? _timeZoneTask;

    public QuarkBrowserTimeZoneService(IQuarkBrowserTimeZoneInterop interop, ILogger<QuarkBrowserTimeZoneService> logger)
    {
        _interop = interop;
        _logger = logger;
    }


    public ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default)
    {
        var task = _timeZoneTask;

        if (task is null)
        {
            lock (_sync)
            {
                task = _timeZoneTask;

                if (task is null)
                {
                    task = DetectTimeZone();
                    _timeZoneTask = task;
                }
            }
        }

        return cancellationToken.CanBeCanceled ? new ValueTask<string?>(task.WaitAsync(cancellationToken)) : new ValueTask<string?>(task);
    }

    private async Task<string?> DetectTimeZone()
    {
        try
        {
            var id = await _interop.GetTimeZoneId();

            if (QuarkDateTimeZoneResolver.TryResolve(id, out _))
                return id;

            return null;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            _logger.LogDebug(exception, "Could not detect browser time zone.");
            return null;
        }
    }
}
