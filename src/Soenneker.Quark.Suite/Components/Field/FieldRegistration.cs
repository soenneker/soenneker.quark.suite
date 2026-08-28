using System;

namespace Soenneker.Quark;

internal sealed class FieldRegistration : IDisposable
{
    private Action? _dispose;

    public FieldRegistration(Action dispose)
    {
        _dispose = dispose;
    }

    public void Dispose()
    {
        var dispose = _dispose;
        _dispose = null;
        dispose?.Invoke();
    }
}
