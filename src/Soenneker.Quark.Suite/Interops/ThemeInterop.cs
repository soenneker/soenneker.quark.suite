using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IThemeInterop"/>
public sealed class ThemeInterop : IThemeInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "/_content/Soenneker.Quark.Suite/js/themeinterop.js";
    private IJSObjectReference? _module;

    public ThemeInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private async ValueTask InitializeResources(CancellationToken token)
    {
        _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", token, _modulePath);
    }

    private async ValueTask<IJSObjectReference> GetModule(CancellationToken cancellationToken)
    {
        _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken, _modulePath);
        return _module;
    }

    public async ValueTask<bool> Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            return await module.InvokeAsync<bool>("initialize", linked);
        }
    }

    public async ValueTask<bool> Toggle(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            return await module.InvokeAsync<bool>("toggle", linked);
        }
    }

    public async ValueTask<bool> GetIsDarkAsync(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            return await module.InvokeAsync<bool>("resolveIsDark", linked);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
            await _module.DisposeAsync();

        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
