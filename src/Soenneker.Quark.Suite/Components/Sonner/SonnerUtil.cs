using System;
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

    public ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Toast(title, configure);
    }

    public ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Toast(content, configure);
    }

    public ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Success(title, configure);
    }

    public ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Info(title, configure);
    }

    public ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Warning(title, configure);
    }

    public ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Error(title, configure);
    }

    public ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Loading(title, configure);
    }

    public ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Custom(content, configure);
    }

    public ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options)
    {
        return _sonner.Promise(task, options);
    }

    public ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options)
    {
        return _sonner.Promise(taskFactory, options);
    }

    public ValueTask Dismiss(string? id = null)
    {
        return _sonner.Dismiss(id);
    }
}
