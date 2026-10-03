using System;

namespace Soenneker.Quark;

internal sealed class OverlayLabelRegistration : IDisposable
{
    private OverlayLabelContext? _context;
    private readonly bool _title;

    public OverlayLabelRegistration(OverlayLabelContext context, bool title)
    {
        _context = context;
        _title = title;
    }

    public void Dispose()
    {
        var context = _context;
        _context = null;
        context?.Unregister(_title);
    }
}
