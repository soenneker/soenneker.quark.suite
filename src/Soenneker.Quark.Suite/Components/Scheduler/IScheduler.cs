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
    /// <summary>Disables creation and mutation.</summary>
    bool ReadOnly { get; set; }
}
