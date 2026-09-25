using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Quark;

public sealed class TablesInterop : ITablesInterop
{
    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/tablesinterop.js";
    private readonly IModuleImportUtil _moduleImportUtil;

    public TablesInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

    public async ValueTask StartAdaptiveLayout(ElementReference element, ElementReference columns, TableColumnSizingOptions options, CancellationToken cancellationToken = default)
    {
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidAsync("initialize", cancellationToken, element, columns, JsonSerializer.SerializeToElement(options, QuarkInteropJsonContext.Default.TableColumnSizingOptions));
    }

    public async ValueTask StopAdaptiveLayout(ElementReference element, CancellationToken cancellationToken = default)
    {
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidAsync("destroy", cancellationToken, element);
    }

    public async ValueTask DisposeAsync() => await _moduleImportUtil.DisposeContentModule(_modulePath);
}
