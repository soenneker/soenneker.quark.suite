using System;
using Soenneker.Blazor.Utils.Ids;

namespace Soenneker.Quark;

internal sealed class OverlayLabelContext
{
    private int _titleRegistrations;
    private int _descriptionRegistrations;

    public event Action? StateChanged;

    public OverlayLabelContext(string scope)
    {
        var baseId = BlazorIdGenerator.New(scope);
        ContentId = BlazorIdGenerator.Child(baseId, "content");
        TitleId = BlazorIdGenerator.Child(baseId, "title");
        DescriptionId = BlazorIdGenerator.Child(baseId, "description");
    }

    public string ContentId { get; }

    public string TitleId { get; }

    public string DescriptionId { get; }

    public bool HasTitle => _titleRegistrations > 0;

    public bool HasDescription => _descriptionRegistrations > 0;

    public IDisposable RegisterTitle()
    {
        _titleRegistrations++;
        StateChanged?.Invoke();
        return new OverlayLabelRegistration(this, title: true);
    }

    public IDisposable RegisterDescription()
    {
        _descriptionRegistrations++;
        StateChanged?.Invoke();
        return new OverlayLabelRegistration(this, title: false);
    }

    internal void Unregister(bool title)
    {
        if (title)
            _titleRegistrations--;
        else
            _descriptionRegistrations--;

        StateChanged?.Invoke();
    }
}
