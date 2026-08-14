using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Coordinates date/time component updates through one scoped timer loop.
/// </summary>
public interface IQuarkDateTimeScheduler : IAsyncDisposable
{
    /// <summary>
    /// Registers a component update and returns a handle that can be rescheduled or disposed.
    /// </summary>
    IQuarkDateTimeScheduleRegistration Register(Func<DateTimeOffset, TimeSpan?> getNextInterval,
        Func<DateTimeOffset, ValueTask> callback);
}

/// <summary>
/// Represents one scheduled date/time component update.
/// </summary>
public interface IQuarkDateTimeScheduleRegistration : IDisposable
{
    /// <summary>
    /// Re-evaluates the next update time after component parameters change.
    /// </summary>
    void Reschedule();
}
