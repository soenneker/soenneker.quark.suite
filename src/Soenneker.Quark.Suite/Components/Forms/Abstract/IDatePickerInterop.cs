using Microsoft.AspNetCore.Components;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Contract for JS interop used by DatePicker / DateTimePicker (e.g. init, attach, outside-click).
/// </summary>
public interface IDatePickerInterop
{
    /// <summary>
    /// Ensures any required resources are loaded and ready.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts tracking a picker panel against its trigger and keeps the panel positioned in the viewport.
    /// </summary>
    ValueTask ObservePosition(string pickerId, ElementReference trigger, ElementReference content, string side, string align, int sideOffset = 4,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops tracking the picker panel position.
    /// </summary>
    ValueTask StopObserving(string pickerId, CancellationToken cancellationToken = default);
}
