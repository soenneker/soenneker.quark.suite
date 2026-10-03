using System;

namespace Soenneker.Quark;

internal sealed class FieldRegistration : IDisposable
{
    private FieldContext? _context;
    private readonly bool _error;

    public FieldRegistration(FieldContext context, bool error)
    {
        _context = context;
        _error = error;
    }

    public void Dispose()
    {
        var context = _context;
        _context = null;
        context?.Unregister(_error);
    }
}
