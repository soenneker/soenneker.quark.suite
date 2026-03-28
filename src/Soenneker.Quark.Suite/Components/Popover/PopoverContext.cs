using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal sealed class PopoverContext
{
    private readonly Func<bool> _getIsOpen;
    private readonly Func<bool, Task> _setOpen;
    private ElementReference _triggerElement;
    private bool _hasTriggerElement;

    public PopoverContext(Func<bool> getIsOpen, Func<bool, Task> setOpen)
    {
        _getIsOpen = getIsOpen;
        _setOpen = setOpen;

        var id = Guid.NewGuid().ToString("N");
        TriggerId = $"popover-trigger-{id}";
        ContentId = $"popover-content-{id}";
    }

    public string TriggerId { get; }

    public string ContentId { get; }

    public bool IsOpen => _getIsOpen();

    public bool HasTriggerElement => _hasTriggerElement;

    public ElementReference TriggerElement => _triggerElement;

    public void SetTriggerElement(ElementReference elementReference)
    {
        _triggerElement = elementReference;
        _hasTriggerElement = true;
    }

    public Task Open()
    {
        return _setOpen(true);
    }

    public Task Close()
    {
        return _setOpen(false);
    }

    public Task Toggle()
    {
        return _setOpen(!IsOpen);
    }
}
