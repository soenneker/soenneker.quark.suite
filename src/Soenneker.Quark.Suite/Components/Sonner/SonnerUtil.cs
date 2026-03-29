using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="ISonnerUtil"/>
public sealed class SonnerUtil : ISonnerUtil
{
    private readonly ISonnerService _sonner;

    public SonnerUtil(ISonnerService sonner)
    {
        _sonner = sonner;
    }

    public ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Toast(title, configure, cancellationToken);
    }

    public ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Toast(content, configure, cancellationToken);
    }

    public ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Success(title, configure, cancellationToken);
    }

    public ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Info(title, configure, cancellationToken);
    }

    public ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Warning(title, configure, cancellationToken);
    }

    public ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Error(title, configure, cancellationToken);
    }

    public ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Loading(title, configure, cancellationToken);
    }

    public ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Custom(content, configure, cancellationToken);
    }

    public ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options, CancellationToken cancellationToken = default)
    {
        return _sonner.Promise(task, options, cancellationToken);
    }

    public ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options, CancellationToken cancellationToken = default)
    {
        return _sonner.Promise(taskFactory, options, cancellationToken);
    }

    public ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default)
    {
        return _sonner.Dismiss(id, cancellationToken);
    }
}
