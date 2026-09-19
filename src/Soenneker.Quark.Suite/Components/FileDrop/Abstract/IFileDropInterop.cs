using Microsoft.AspNetCore.Components;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>Owns browser event registration for file drop targets.</summary>
public interface IFileDropInterop
{
    /// <summary>Registers or updates a target. Sets data-drag-active during file drags; text drags remain untouched.</summary>
    ValueTask Register(ElementReference target, string inputId, CancellationToken cancellationToken = default);
    /// <summary>Removes the target's listeners and clears its drag state.</summary>
    ValueTask Unregister(ElementReference target, CancellationToken cancellationToken = default);
}
