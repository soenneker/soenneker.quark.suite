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
    private int _titleRegistrations;
    private int _descriptionRegistrations;

    public event Action? StateChanged;

    public PopoverContext(Func<bool> getIsOpen, Func<bool, Task> setOpen)
    {
        _getIsOpen = getIsOpen;
        _setOpen = setOpen;

        var id = Guid.NewGuid().ToString("N");
        TriggerId = $"popover-trigger-{id}";
        ContentId = $"popover-content-{id}";
        TitleId = $"popover-title-{id}";
        DescriptionId = $"popover-description-{id}";
    }

    public string TriggerId { get; }

    public string ContentId { get; }

    public string TitleId { get; }

    public string DescriptionId { get; }

    public bool HasTitle => _titleRegistrations > 0;

    public bool HasDescription => _descriptionRegistrations > 0;

    /// <summary>Radix <c>modal</c>: when <c>true</c>, content is exposed as a modal dialog for assistive tech.</summary>
    public bool Modal { get; set; }

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

    public IDisposable RegisterTitle()
    {
        _titleRegistrations++;
        StateChanged?.Invoke();
        return new Registration(() =>
        {
            _titleRegistrations--;
            StateChanged?.Invoke();
        });
    }

    public IDisposable RegisterDescription()
    {
        _descriptionRegistrations++;
        StateChanged?.Invoke();
        return new Registration(() =>
        {
            _descriptionRegistrations--;
            StateChanged?.Invoke();
        });
    }

    private sealed class Registration : IDisposable
    {
        private Action? dispose;

        public Registration(Action disposeAction)
        {
            dispose = disposeAction;
        }

        public void Dispose()
        {
            var d = dispose;
            dispose = null;
            d?.Invoke();
        }
    }
}
