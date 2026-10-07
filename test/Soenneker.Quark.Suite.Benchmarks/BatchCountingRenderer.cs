using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;

namespace Soenneker.Quark.Suite.Benchmarks;

// Measures Blazor's render/diff pipeline without a bUnit/AngleSharp DOM.
#pragma warning disable BL0006
public sealed class BatchCountingRenderer(IServiceProvider services, ILoggerFactory loggerFactory) : Renderer(services, loggerFactory)
{
    public override Dispatcher Dispatcher { get; } = Dispatcher.CreateDefault();
    public int UpdatedComponents { get; private set; }
    public int ReferenceFrames { get; private set; }
    public int Attach(Microsoft.AspNetCore.Components.IComponent component) => AssignRootComponentId(component);
    public Task Render(int id, ParameterView parameters) => RenderRootComponentAsync(id, parameters);
    public void ResetCounters() { UpdatedComponents = 0; ReferenceFrames = 0; }
    protected override void HandleException(Exception exception) => throw new InvalidOperationException("Render failed.", exception);
    protected override Task UpdateDisplayAsync(in RenderBatch batch)
    {
        UpdatedComponents += batch.UpdatedComponents.Count;
        ReferenceFrames += batch.ReferenceFrames.Count;
        return Task.CompletedTask;
    }
}
#pragma warning restore BL0006
