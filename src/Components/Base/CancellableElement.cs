using Microsoft.AspNetCore.Components;
using Soenneker.Quark;

namespace Soenneker.Quark;

///<inheritdoc cref="ICancellableElement"/>
public abstract class CancellableElement : CancellableComponent, ICancellableElement
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
