using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="IInteractiveSurfaceElement"/>
public abstract class InteractiveSurfaceElement : InteractiveSurface, IInteractiveSurfaceElement
{
    public RenderFragment? ChildContent { get; set; }
}

