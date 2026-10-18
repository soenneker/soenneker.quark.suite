using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace Soenneker.Quark;
/// <summary>Owns pointer, selection-checkbox and touch interaction for workflow elements.</summary>
public interface IWorkflowInteractionInterop
{
    /// <summary>Registers delegated interaction handlers for a grid or scheduler root.</summary>
    ValueTask Mount<T>(ElementReference element, DotNetObjectReference<T> callback) where T : class;
    /// <summary>Updates the native indeterminate property after rendering.</summary>
    ValueTask Update(ElementReference element);
    /// <summary>Removes listeners and cancels an in-progress gesture.</summary>
    ValueTask Unmount(ElementReference element);
}
