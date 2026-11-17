using System.Text.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

public sealed class SwipeAreaInterop : ISwipeAreaInterop, IAsyncDisposable
{
    private readonly string ModulePath;

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public SwipeAreaInterop(IModuleImportUtil moduleImportUtil, QuarkOptions? quarkOptions = null)
    {
        ModulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/swipeareainterop.js", quarkOptions);
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Configure(ElementReference element, DotNetObjectReference<SwipeArea> callback,
        SwipeAreaInteropOptions options, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            await module.InvokeVoidWithContextAsync("configure", linked, element, callback,
                JsonSerializer.SerializeToElement(options, QuarkInteropJsonContext.Default.SwipeAreaInteropOptions));
        }
    }

    public async ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            await module.InvokeVoidWithContextAsync("destroy", linked, element);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(ModulePath);
    }

}
