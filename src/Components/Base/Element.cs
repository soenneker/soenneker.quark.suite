using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="IElement"/>
public abstract class Element : Component, IElement
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
