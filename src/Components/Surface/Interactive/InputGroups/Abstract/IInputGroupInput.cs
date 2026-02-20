using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public interface IInputGroupInput : IElement
{
    string? Value { get; set; }
    EventCallback<string?> ValueChanged { get; set; }
}
