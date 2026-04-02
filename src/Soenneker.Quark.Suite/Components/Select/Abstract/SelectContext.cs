using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal sealed class SelectContext
{
    private ElementReference _triggerElement;
    private bool _hasTriggerElement;

    public required Func<bool> IsOpen { get; init; }
    public required Func<bool> HasValue { get; init; }
    public required Func<string?> DisplayText { get; init; }
    public required Func<bool> IsDisabled { get; init; }
    public required Func<bool> IsInvalid { get; init; }
    public required Func<Task> ToggleAsync { get; init; }
    public required Func<Task> CloseAsync { get; init; }
    public required string TriggerId { get; init; }
    public required string ContentId { get; init; }

    public bool HasTriggerElement => _hasTriggerElement;

    public ElementReference TriggerElement => _triggerElement;

    public void SetTriggerElement(ElementReference elementReference)
    {
        _triggerElement = elementReference;
        _hasTriggerElement = true;
    }
}
