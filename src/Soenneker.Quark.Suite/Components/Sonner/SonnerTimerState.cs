using System;
using System.Threading;

namespace Soenneker.Quark;

internal sealed class SonnerTimerState
{
    public CancellationTokenSource? CancellationTokenSource { get; set; }

    public DateTimeOffset StartedAt { get; set; }

    public int RemainingMs { get; set; }

    public bool Paused { get; set; }
}
