using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>Coordinates calendar keyboard navigation and focus without global event listeners.</summary>
public interface ICalendarInterop : IAsyncDisposable
{
    /// <summary>Attaches the keyboard handler to a calendar.</summary>
    ValueTask Initialize(ElementReference element, DotNetObjectReference<Calendar> receiver);

    /// <summary>Focuses the enabled button for an ISO date within this calendar.</summary>
    ValueTask Focus(ElementReference element, string date);

    /// <summary>Removes the calendar's keyboard handler.</summary>
    ValueTask Destroy(ElementReference element);
}
