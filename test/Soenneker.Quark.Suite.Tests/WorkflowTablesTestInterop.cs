using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark.Suite.Tests;
internal sealed class WorkflowTablesTestInterop : ITablesInterop
{
    public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask StartAdaptiveLayout(ElementReference element, ElementReference columns, TableColumnSizingOptions options, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask StopAdaptiveLayout(ElementReference element, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
