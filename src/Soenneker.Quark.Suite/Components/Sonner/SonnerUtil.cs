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

    public string Toast(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Toast(title, configure);
    }

    public string Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Toast(content, configure);
    }

    public string Success(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Success(title, configure);
    }

    public string Info(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Info(title, configure);
    }

    public string Warning(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Warning(title, configure);
    }

    public string Error(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Error(title, configure);
    }

    public string Loading(string title, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Loading(title, configure);
    }

    public string Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null)
    {
        return _sonner.Custom(content, configure);
    }

    public string Promise(Task task, SonnerPromiseOptions options)
    {
        return _sonner.Promise(task, options);
    }

    public string Promise(Func<Task> taskFactory, SonnerPromiseOptions options)
    {
        return _sonner.Promise(taskFactory, options);
    }

    public void Dismiss(string? id = null)
    {
        _sonner.Dismiss(id);
    }
}
