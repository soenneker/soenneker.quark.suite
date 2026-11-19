using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>A timezone-aware appointment scheduler with month, agenda, day and resource-lane week views.</summary>
public interface IScheduler : IElement
{
    /// <summary>Maximum resource lanes displayed per page. Defaults to eight.</summary>
    int MaximumVisibleResources { get; set; }

    /// <summary>Events in the loaded range, identified by unique stable IDs.</summary>
    IReadOnlyList<SchedulerEvent> Events { get; set; }
    /// <summary>Receives a replacement list after a successful create, edit, move, resize or delete.</summary>
    EventCallback<IReadOnlyList<SchedulerEvent>> EventsChanged { get; set; }
    /// <summary>Runs when a time is picked. Booking mode supplies the start and exclusive end and awaits the handler before accepting another selection. In calendar management views this callback replaces the built-in editor. Available in read-only mode.</summary>
    EventCallback<SchedulerSlot> SlotSelected { get; set; }
    /// <summary>Handles an existing event selection in place of the built-in editor. Available in read-only mode.</summary>
    EventCallback<SchedulerEvent> EventSelected { get; set; }
    /// <summary>Display timezone. Defaults to UTC.</summary>
    TimeZoneInfo TimeZone { get; set; }
    /// <summary>Focused calendar date.</summary>
    DateOnly Date { get; set; }
    /// <summary>Receives navigation changes so applications can load the corresponding range.</summary>
    EventCallback<DateOnly> DateChanged { get; set; }
    /// <summary>Presentation mode.</summary>
    SchedulerView View { get; set; }
    /// <summary>Receives presentation changes.</summary>
    EventCallback<SchedulerView> ViewChanged { get; set; }
    /// <summary>Optional resource lanes; unassigned events remain visible.</summary>
    IReadOnlyList<SchedulerResource> Resources { get; set; }
    /// <summary>Optional persistence callback; throw to retain original data and show an error.</summary>
    Func<SchedulerEvent, CancellationToken, ValueTask>? Save { get; set; }
    /// <summary>Optional deletion persistence callback.</summary>
    Func<SchedulerEvent, CancellationToken, ValueTask>? Delete { get; set; }
    /// <summary>Displays meeting details, a calendar and available appointment starts by default. Set to false for event management.</summary>
    bool BookingMode { get; set; }
    /// <summary>Title displayed above the meeting details on the booking page.</summary>
    string? BookingTitle { get; set; }
    /// <summary>Receives the edited title so the host application can persist it.</summary>
    EventCallback<string?> BookingTitleChanged { get; set; }
    /// <summary>Enables the booking title input for authorized hosts. Defaults to false.</summary>
    bool CanEditTitle { get; set; }
    /// <summary>Optional meeting content displayed above the duration and time zone.</summary>
    RenderFragment? MeetingDetails { get; set; }
    /// <summary>Optional fields displayed below the booking time zone. Bind their values to the calling component's booking model.</summary>
    RenderFragment? BookingFields { get; set; }
    /// <summary>Appointment duration used for conflict checks in booking mode.</summary>
    TimeSpan BookingDuration { get; set; }
    /// <summary>Receives the duration selected on the booking page.</summary>
    EventCallback<TimeSpan> BookingDurationChanged { get; set; }
    /// <summary>Selected booking start highlighted in the list.</summary>
    DateTimeOffset? SelectedStart { get; set; }
    /// <summary>Disables creation and mutation.</summary>
    bool ReadOnly { get; set; }
}
