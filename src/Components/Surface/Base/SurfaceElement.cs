using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="ISurfaceElement"/>
public abstract class SurfaceElement : Surface, ISurfaceElement
{
    public RenderFragment? ChildContent { get; set; }
}