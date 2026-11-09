using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Measures collapsible content and maintains numeric animation endpoints.</summary>
public interface ICollapseInterop
{
    /// <summary>Initializes or updates an element's expansion state and animation axis.</summary>
    ValueTask Update(ElementReference element, bool expanded, bool horizontal, CancellationToken cancellationToken = default);

    /// <summary>Disconnects content measurement observers for the element.</summary>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
