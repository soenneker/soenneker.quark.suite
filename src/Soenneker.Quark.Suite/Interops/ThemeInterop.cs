using Microsoft.JSInterop;
using System;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class ThemeInterop : IThemeInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();
    private DotNetObjectReference<ThemeInterop>? _callback;
    public bool IsDark { get; private set; }
    public event Action<bool>? ThemeChanged;

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/themeinterop.js";

    public ThemeInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask<bool> Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            _callback ??= DotNetObjectReference.Create(this);
            await module.InvokeVoidAsync("registerThemeChangedCallback", linked, _callback);
            var isDark = await module.InvokeAsync<bool>("initialize", linked);
            OnThemeChanged(isDark);
            return isDark;
        }
    }

    public async ValueTask<bool> Toggle(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            var isDark = await module.InvokeAsync<bool>("toggle", linked);
            OnThemeChanged(isDark);
            return isDark;
        }
    }

    public async ValueTask<bool> GetIsDark(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<bool>("resolveIsDark", linked);
        }
    }

    public async ValueTask<bool> UseSystem(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            var isDark = await module.InvokeAsync<bool>("useSystem", linked);
            OnThemeChanged(isDark);
            return isDark;
        }
    }

    [JSInvokable]
    public void OnThemeChanged(bool isDark)
    {
        if (IsDark == isDark) return;
        IsDark = isDark;
        ThemeChanged?.Invoke(isDark);
    }

    public async ValueTask DisposeAsync()
    {
        if (_callback is not null)
        {
            try
            {
                var module = await _moduleImportUtil.GetContentModuleReference(_modulePath);
                await module.InvokeVoidAsync("unregisterThemeChangedCallback", _callback);
            }
            catch (JSDisconnectedException) { }
            finally { _callback.Dispose(); }
        }
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
