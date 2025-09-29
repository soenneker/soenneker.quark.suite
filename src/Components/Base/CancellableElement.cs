using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Components.Base.Abstract;

namespace Soenneker.Quark.Components.Base;

///<inheritdoc cref="ICancellableElement"/>
public abstract class CancellableElement : CancellableComponent, ICancellableElement
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
