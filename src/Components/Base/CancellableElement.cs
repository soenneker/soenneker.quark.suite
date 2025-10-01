using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="ICancellableElement"/>
public abstract class CancellableElement : CancellableComponent, ICancellableElement
{
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}
