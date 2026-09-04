using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IQuarkDateTimeScheduleRegistration" />
internal sealed class QuarkDateTimeScheduleRegistration : IQuarkDateTimeScheduleRegistration
{
    private readonly QuarkDateTimeScheduler _owner;

    internal QuarkDateTimeScheduleRegistration(QuarkDateTimeScheduler owner, Func<DateTimeOffset, TimeSpan?> getNextInterval,
        Func<DateTimeOffset, ValueTask> callback)
    {
        _owner = owner;
        GetNextInterval = getNextInterval;
        Callback = callback;
    }

    internal Func<DateTimeOffset, TimeSpan?> GetNextInterval { get; }

    internal Func<DateTimeOffset, ValueTask> Callback { get; }

    internal DateTimeOffset? NextUpdate { get; set; }

    internal int Version { get; set; }

    internal bool Disposed { get; set; }

    public void Reschedule() => _owner.Reschedule(this);

    public void Dispose() => _owner.Unregister(this);
}
