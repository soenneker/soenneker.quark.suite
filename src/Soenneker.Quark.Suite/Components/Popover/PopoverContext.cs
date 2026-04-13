using System;
namespace Soenneker.Quark;

internal sealed class PopoverContext
{
    private int _titleRegistrations;
    private int _descriptionRegistrations;

    public event Action? StateChanged;

    public PopoverContext()
    {
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
