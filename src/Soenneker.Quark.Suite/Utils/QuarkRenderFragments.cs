using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal static class QuarkRenderFragments
{
    // Forwarding wrappers must still supply non-null content when no content was
    // specified, since downstream components can distinguish null from empty.
    internal static readonly RenderFragment Empty = static _ => { };
}
