using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace Soenneker.Quark.Suite.Tests;
internal sealed class WorkflowInteractionTestInterop : IWorkflowInteractionInterop
{
    public ValueTask Mount<T>(ElementReference element, DotNetObjectReference<T> callback) where T : class => ValueTask.CompletedTask;
    public ValueTask Update(ElementReference element) => ValueTask.CompletedTask;
    public ValueTask Unmount(ElementReference element) => ValueTask.CompletedTask;
}
