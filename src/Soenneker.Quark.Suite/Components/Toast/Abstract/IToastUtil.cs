using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public interface IToastUtil
{
    event Action? Changed;

    IReadOnlyList<ToastEntry> Toasts { get; }

    ValueTask<string> Toast(string title, Action<ToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default);

    ValueTask Remove(string? id = null, CancellationToken cancellationToken = default);
}
