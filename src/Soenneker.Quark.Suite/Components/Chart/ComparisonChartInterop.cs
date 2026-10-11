using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

public sealed class ComparisonChartInterop : IComparisonChartInterop, IAsyncDisposable
{
    private readonly string ModulePath;

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public ComparisonChartInterop(IModuleImportUtil moduleImportUtil, QuarkOptions? quarkOptions = null)
    {
        ModulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/comparisonchartinterop.js", quarkOptions);
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Initialize(ElementReference element, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            await module.InvokeVoidAsync("initialize", linked, element);
        }
    }

    public async ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            await module.InvokeVoidAsync("destroy", linked, element);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(ModulePath);
    }

}
