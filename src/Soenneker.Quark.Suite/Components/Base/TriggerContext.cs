using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class TriggerContext
{
    public string? TriggerId { get; init; }

    public bool IsOpen { get; init; }

    public string? AriaHasPopup { get; init; }

    public string? AriaControls { get; init; }

    public string? AriaDescribedBy { get; init; }

    public Func<Task>? Toggle { get; init; }

    public Func<Task>? Open { get; init; }

    public Func<Task>? Close { get; init; }

    public Func<Task>? OnMouseEnter { get; init; }

    public Func<Task>? OnMouseLeave { get; init; }

    public Func<Task>? OnFocusIn { get; init; }

    public Func<Task>? OnFocusOut { get; init; }
}
