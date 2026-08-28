using System;

namespace Soenneker.Quark;

internal sealed class OverlayLabelRegistration : IDisposable
{
    private Action? _dispose;

    public OverlayLabelRegistration(Action dispose)
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
